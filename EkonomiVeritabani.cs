using Rocket.API;
using Rocket.Unturned.Player;
using RPEkonomi.Data;
using System.Collections.Generic;

namespace RPEkonomi.Commands
{
    public class TransferCommand : IRocketCommand
    {
        public string Name => "transfer";
        public string Help => "Başka bir oyuncuya para gönderir. Vergi uygulanır.";
        public string Syntax => "/transfer <oyuncu> <miktar>";
        public List<string> Aliases => new List<string> { "gonder", "pay" };
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public List<string> Permissions => new List<string> { "rpEkonomi.transfer" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var player = (UnturnedPlayer)caller;

            if (command.Length < 2)
            {
                player.SendChat($"[Ekonomi] Kullanım: {Syntax}");
                return;
            }

            string hedefAd = command[0];
            UnturnedPlayer hedef = UnturnedPlayer.FromName(hedefAd);

            if (hedef == null)
            {
                player.SendChat($"[Ekonomi] '{hedefAd}' adlı oyuncu bulunamadı!");
                return;
            }

            if (hedef.CSteamID == player.CSteamID)
            {
                player.SendChat("[Ekonomi] Kendine para gönderemezsin!");
                return;
            }

            if (!decimal.TryParse(command[1], out decimal miktar) || miktar <= 0)
            {
                player.SendChat("[Ekonomi] Geçersiz miktar! Pozitif bir sayı gir.");
                return;
            }

            var config = RPEkonomiPlugin.Instance.Configuration.Instance;
            EkonomiVeritabani db = RPEkonomiPlugin.Instance.Veritabani;

            if (miktar < config.MinTransferMiktar)
            {
                player.SendChat($"[Ekonomi] Minimum transfer miktarı: {config.MinTransferMiktar}{config.ParaBirimiSembol}");
                return;
            }

            decimal vergi = System.Math.Round(miktar * config.TransferVergiYuzdesi / 100, 2);
            decimal hedefAlacak = miktar - vergi;

            if (!db.ParaDus(player.CSteamID.ToString(), miktar))
            {
                decimal mevcutBakiye = db.ParaAl(player.CSteamID.ToString());
                player.SendChat($"[Ekonomi] Yetersiz bakiye! Bakiyen: {mevcutBakiye}{config.ParaBirimiSembol}");
                return;
            }

            db.ParaEkle(hedef.CSteamID.ToString(), hedefAlacak);
            db.Kaydet();

            player.SendChat($"[Ekonomi] {hedef.DisplayName} oyuncusuna {hedefAlacak}{config.ParaBirimiSembol} gönderildi. (Vergi: {vergi}{config.ParaBirimiSembol})");
            hedef.SendChat($"[Ekonomi] {player.DisplayName} sana {hedefAlacak}{config.ParaBirimiSembol} gönderdi!");
        }
    }
}
