# RPEkonomi - Unturned RocketMod Para/Ekonomi Plugin

## Özellikler
- Oyuncu başına kalıcı bakiye (XML veritabanı)
- Otomatik kayıt (ilk girişte başlangıç parası)
- Para transferi (%5 vergi, ayarlanabilir)
- Admin para yönetimi (ekle / çıkar / ayarla / bilgi)
- En zengin oyuncular liderboardı
- Thread-safe veri kilitleme
- Tam yapılandırılabilir config

---

## Komutlar

| Komut | Açıklama | İzin |
|-------|----------|------|
| `/bakiye [oyuncu]` | Bakiyeni veya başkasının bakiyesini göster | `rpEkonomi.bakiye` |
| `/transfer <oyuncu> <miktar>` | Oyuncuya para gönder (vergili) | `rpEkonomi.transfer` |
| `/zenginler` | Top 10 zengin oyuncu listesi | `rpEkonomi.zenginler` |
| `/adminpara ekle <oyuncu> <miktar>` | Admin: para ekle | `rpEkonomi.admin` |
| `/adminpara cikar <oyuncu> <miktar>` | Admin: para çıkar | `rpEkonomi.admin` |
| `/adminpara ayarla <oyuncu> <miktar>` | Admin: bakiye ayarla | `rpEkonomi.admin` |
| `/adminpara bilgi <oyuncu>` | Admin: oyuncu istatistikleri | `rpEkonomi.admin` |

---

## Derleme (Windows - Visual Studio)

1. Visual Studio 2019/2022 yükle
2. `Libs/` klasörü oluştur, aşağıdaki DLL'leri Unturned sunucu klasöründen kopyala:
   - `Rocket.Core.dll`
   - `Rocket.Unturned.dll`
   - `Assembly-CSharp.dll`
   - `UnityEngine.CoreModule.dll`
3. `RPEkonomi.csproj` dosyasını Visual Studio ile aç
4. **Build → Build Solution** (Ctrl+Shift+B)
5. `bin/Debug/net461/RPEkonomi.dll` dosyası oluşur

### Alternatif: dotnet CLI ile derleme
```bash
# Libs/ klasörüne DLL'leri kopyaladıktan sonra:
dotnet build RPEkonomi.csproj -c Release
# Çıktı: bin/Release/net461/RPEkonomi.dll
```

---

## Kurulum

1. Derlenen `RPEkonomi.dll` dosyasını kopyala:
   ```
   Unturned/Rocket/Plugins/RPEkonomi.dll
   ```
2. Sunucuyu başlat → config otomatik oluşur:
   ```
   Unturned/Rocket/Plugins/RPEkonomi/config.xml
   ```

---

## Config (config.xml)

```xml
<?xml version="1.0" encoding="utf-8"?>
<EkonomiConfig>
  <BaslangicParasi>1000</BaslangicParasi>
  <MaxPara>10000000</MaxPara>
  <MinTransferMiktar>1</MinTransferMiktar>
  <TransferVergiYuzdesi>5</TransferVergiYuzdesi>
  <ParaBirimiSembol>₺</ParaBirimiSembol>
</EkonomiConfig>
```

---

## İzinler (permissions.xml)

```xml
<Group Id="default">
  <Permissions>
    <Permission>rpEkonomi.bakiye</Permission>
    <Permission>rpEkonomi.transfer</Permission>
    <Permission>rpEkonomi.zenginler</Permission>
  </Permissions>
</Group>

<Group Id="admin">
  <Permissions>
    <Permission>rpEkonomi.admin</Permission>
  </Permissions>
</Group>
```

---

## Veri Dosyası

Veriler burada saklanır:
```
Unturned/Rocket/Plugins/RPEkonomi/ekonomi_veri.xml
```
Sunucu kapanışında ve her transfer sonrası otomatik kaydedilir.
