using Fotootof.Layouts.Windows.Forms.Album;
using XtrmAddons.Fotootof.Lib.SQLite.Database.Data.Tables.Entities;

namespace Fotootof.Layouts
{
    public static class WindowLayouts
    {
        public static bool? OpenDialogAlbum(AlbumEntity albumEntity)
        {
            WindowFormAlbumLayout dlg = new WindowFormAlbumLayout(new AlbumEntity());
            return dlg.ShowDialog();
        }
    }
}
