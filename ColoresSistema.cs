using System.Drawing;

namespace Nolkata_Final
{
    public static class ColoresSistema
    {
        // ==================== COLORES DEL LOGOTIPO NOLKATA MARINE ====================
        public static Color ColorPrincipal = Color.FromArgb(29, 112, 184);   // #1D70B8 Azul Real
        public static Color ColorSecundario = Color.FromArgb(96, 153, 192);  // #6099C0 Azul Acero
        public static Color ColorAcento = Color.FromArgb(0, 113, 188);       // #0071BC Azul Brillante
        public static Color ColorVerdeMenta = Color.FromArgb(161, 212, 177); // #A1D4B1 Verde Menta
        public static Color ColorFondo = Color.FromArgb(240, 249, 246);      // #F0F9F6 Blanco Hielo
        public static Color ColorBlanco = Color.FromArgb(255, 255, 255);     // #FFFFFF Blanco
        public static Color ColorSeleccion = Color.FromArgb(209, 239, 254);  // #D1EFFE Azul Glaciar

        // ==================== COLORES DE TEXTO ====================
        public static Color ColorTextoNormal = Color.FromArgb(51, 51, 51);   // #333333
        public static Color ColorTextoClaro = Color.FromArgb(255, 255, 255); // #FFFFFF

        // ==================== COLORES DE ESTADOS ====================
        public static Color EstadoPendiente = Color.FromArgb(255, 99, 99);    // #ff6363
        public static Color EstadoEnProceso = Color.FromArgb(255, 161, 133);  // #ffa185
        public static Color EstadoEntregado = Color.FromArgb(212, 255, 133);  // #d4ff85
        public static Color EstadoAlerta = Color.FromArgb(237, 88, 88);       // #ed5858
    }
}