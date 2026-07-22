# BelajarGitBlazor

Project **Blazor Web App (.NET 8, Interactive Server)** yang sederhana — dibuat khusus sebagai
media latihan **Git** dari pemula hingga pro.

## Struktur Project

```
BelajarGitBlazor/
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor
│   │   └── NavMenu.razor
│   ├── Pages/
│   │   ├── Home.razor
│   │   ├── Counter.razor
│   │   └── Weather.razor
│   ├── App.razor
│   ├── Routes.razor
│   └── _Imports.razor
├── wwwroot/
│   └── css/app.css
├── appsettings.json
├── appsettings.Development.json
├── BelajarGitBlazor.csproj
├── Program.cs
└── .gitignore
```

## Prasyarat

- [.NET 8 SDK](https://dotnet.microsoft.com/download) terinstal di komputer kamu
- Cek dengan: `dotnet --version` (harus 8.x)
- Git sudah terinstal (`git --version`)

## Cara Menjalankan

```bash
cd BelajarGitBlazor
dotnet restore
dotnet run
```

Buka browser ke alamat yang muncul di terminal (biasanya `https://localhost:5001` atau sejenisnya).

Ada 3 halaman:
- **Home** (`/`) — halaman sambutan
- **Counter** (`/counter`) — tombol penghitung interaktif
- **Weather** (`/weather`) — tabel data contoh

## Mulai Latihan Git

1. **Inisialisasi repo:**
   ```bash
   git init
   git branch -M main
   git add .
   git commit -m "chore: initial commit - project Blazor dasar"
   ```

2. **Hubungkan ke GitHub** (buat repo kosong dulu di GitHub):
   ```bash
   git remote add origin https://github.com/username/BelajarGitBlazor.git
   git push -u origin main
   ```

3. **Latihan branch — tambahkan tombol Reset di Counter:**
   ```bash
   git checkout -b feature/custom-counter
   ```
   Edit `Components/Pages/Counter.razor`, tambahkan tombol reset (lihat komentar `TODO` di file tersebut), lalu:
   ```bash
   git add .
   git commit -m "feat: tambah tombol reset di halaman counter"
   git push -u origin feature/custom-counter
   ```
   Buka GitHub → buat Pull Request → merge ke `main`.

4. **Latihan konflik, stash, rebase, tag, dan seterusnya** — ikuti **Bagian 8** dari panduan
   *"Panduan Lengkap Git: Dari Pemula hingga Pro"* yang sudah kamu terima sebelumnya. Semua
   langkah di panduan itu dirancang untuk project ini.

## Ide Tantangan Lanjutan

- Tambahkan halaman baru `/about` dan daftarkan di `NavMenu.razor`
- Ubah `Weather.razor` agar mengambil data dari API eksternal (`HttpClient`)
- Praktikkan strategi **Git Flow**: buat branch `develop`, lalu `feature/*` dari `develop`
- Simulasikan `hotfix/*` langsung dari `main` untuk perbaikan darurat

Selamat berlatih! 🚀
