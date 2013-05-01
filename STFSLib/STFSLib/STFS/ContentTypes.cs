using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STFSLib.STFS
{
    public enum ContentTypes
    {
        Arcade = 0xd0000,
        AvatarAsset = 0x9000,
        Cache = 0x40000,
        GameDemo = 0x80000,
        GamerPicture = 0x20000,
        GameTitle = 0xa0000,
        GameTrailer = 0xc0000,
        GameVideo = 0x400000,
        InstalledXbox360Title = 0x4000,
        Installer = 0xb0000,
        IPTVDVR = 0x1000,
        IPTVPauseBuffer = 0x2000,
        LicenseStore = 0xf0000,
        Marketplace = 2,
        Movie = 0x100000,
        MusicVideo = 0x300000,
        PodcastVideo = 0x500000,
        Profile = 0x10000,
        Promotional = 0x400000,
        Publisher = 3,
        SavedGame = 1,
        SocialTitle = 0x6000,
        StorageDownload = 0x50000,
        SystemUpdateStoragePack = 0x8000,
        ThematicSkin = 0x30000,
        TV = 0x200000,
        Unknown = 0x600001,
        Video = 0x90000,
        ViralVideo = 0x600000,
        Xbox360Title = 0x7000,
        XboxDownload = 0x70000,
        XboxSavedGame = 0x60000,
        XboxTitle = 0x5000,
        XNA = 0xe0000,
        XNACommunity = 0x3000
    }
}
