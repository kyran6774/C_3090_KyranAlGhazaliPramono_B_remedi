using System;

namespace UTS_REMIDI
{
    // 1. Class, Object, Attribute, Method
    class Agen
    {
        public string idAgen, namaAgen;
        public int stokAgen;

        public void TampilInfo()
        {
            Console.WriteLine($"[{idAgen}] {namaAgen} - Stok: {stokAgen}");
        }
    }

    // 2, 4, 5. Encapsulation, Polymorphism, Abstract Class
    abstract class ProdukGalon
    {
        private string idProduk, namaProduk;
        private int stokGudang;

        public string IdProduk { get => idProduk; set => idProduk = value; }
        public string NamaProduk { get => namaProduk; set => namaProduk = value; }
        public int StokGudang
        {
            get => stokGudang;
            set { if (value >= 0) stokGudang = value; else Console.WriteLine("Stok tidak boleh minus!"); }
        }

        public void KurangiStok(int jumlah)
        {
            if (jumlah > 0 && stokGudang >= jumlah) stokGudang -= jumlah;
        }

        public abstract int HitungBiaya(int jumlah);
    }

    // 3 & 4. Inheritance & Polymorphism
    class GalonAkua : ProdukGalon
    {
        public int harga = 20000;
        public override int HitungBiaya(int jumlah) => jumlah * harga;
    }

    class GalonBerkeran : ProdukGalon
    {
        public int harga = 12000;
        public string jenisKeran;
        public override int HitungBiaya(int jumlah) => jumlah * harga;
    }

    // 6. Interface
    interface DapatDidistribusikan
    {
        void KirimPaket();
    }

    class DistribusiGalon : DapatDidistribusikan
    {
        public Agen agenTujuan;
        public ProdukGalon produkDikirim;
        public int jumlah;

        public void KirimPaket()
        {
            if (jumlah <= 0 || jumlah > produkDikirim.StokGudang)
            {
                Console.WriteLine("Pengiriman gagal! Jumlah tidak valid atau stok kurang.");
                return;
            }

            produkDikirim.KurangiStok(jumlah);
            agenTujuan.stokAgen += jumlah;
            Console.WriteLine($"Terkirim {jumlah} galon ke {agenTujuan.namaAgen}. Sisa gudang: {produkDikirim.StokGudang}, Stok agen: {agenTujuan.stokAgen}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Nomor 1
            Agen agen1 = new Agen { idAgen = "AG-01", namaAgen = "Markas Pusat", stokAgen = 50 };
            Agen agen2 = new Agen { idAgen = "AG-02", namaAgen = "Cabang Barat", stokAgen = 20 };
            agen1.TampilInfo();
            agen2.TampilInfo();

            // Nomor 2 - 5 (Polymorphism pakai tipe ProdukGalon)
            ProdukGalon p1 = new GalonAkua { IdProduk = "G01", NamaProduk = "Galon Akua", StokGudang = 100 };
            ProdukGalon p2 = new GalonBerkeran { IdProduk = "G02", NamaProduk = "Galon Berkeran", jenisKeran = "Putar", StokGudang = 50 };
            Console.WriteLine($"Biaya 5 {p1.NamaProduk}: Rp{p1.HitungBiaya(5)}");
            Console.WriteLine($"Biaya 5 {p2.NamaProduk}: Rp{p2.HitungBiaya(5)}");

            // Nomor 6
            DistribusiGalon kirim = new DistribusiGalon { agenTujuan = agen1, produkDikirim = p1, jumlah = 15 };
            kirim.KirimPaket();
        }
    }
}
