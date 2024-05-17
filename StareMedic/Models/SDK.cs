using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StareMedic.Models
{
    public class SDK
    {
        public class constantes
        {
            public const int kLongFecha = 24;
            public const int kLongSerie = 12;
            public const int kLongCodigo = 31;
            public const int kLongNomre = 61;
            public const int kLongReferencia = 21;
            public const int kLongDescripcion = 61;
            public const int kLongCuenta = 101;
            public const int kLongMensaje = 3001;
            public const int kLongNombreProducto = 256;
            public const int kLongAbreviatura = 4;
            public const int kLongCodValorCasif = 4;
            public const int kLongDenComercial = 51;
            public const int kLongRepLegal = 51;
            public const int kLongTextoExtra = 51;
            public const int kLongRFC = 21;
            public const int kLongCURP = 21;
            public const int kLongDesCorta = 21;
            public const int kLongNumeroExtInt = 7;
            public const int kLongNumeroExpandido = 31;
            public const int kLongCodigoPostal = 7;
            public const int kLongTelefono = 16;
            public const int kLongEmailWeb = 51;
            public const int kLongSelloSat = 691;
            public const int kSDKLonSerieCertSAT = 190;
            public const int kLongFechaHora = 36;
            public const int kLongSelloCFDI = 691;
            public const int kSDKLongitudUUID = 206;
            public const int kLongitudRegimen = 101;
            public const int kLongitudMoneda = 61;
            public const int kLongitudFolio = 17;
            public const int kLongitudMonto = 31;
            public const int kLongitudLugarExpedicion = 401;
            public const int kLongitudNomBanExtrangero = 255;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct tDocumento
        {
            public double aFolio;
            public int aNumMoneda;
            public double aTipoCambio;
            public double aImporte;
            public double aDescuentoDoc1;
            public double aDescuentoDoc2;
            public int aSistemaOrigen;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string aCodConcepto;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongSerie)]
            public string aSerie;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongFecha)]
            public string aFecha;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string aCodigoCteProv;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string aCodigoAgente;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongReferencia)]
            public string aReferencia;
            public int aAfecta;
            public double aGasto1;
            public double aGasto2;
            public double aGasto3;

        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct tCteProv
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            public string cCodigoCliente;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongNomre)]
            public string cRazonSocial;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongFecha)]
            public string cFechaAlta;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongRFC)]
            public string cRFC;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCURP)] i think we can skip this ones 4 now
            //public string cCURP;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongDenComercial)]
            //public string cDenComercial;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongRepLegal)]
            //public string cRepLegal;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongNomre)]
            //public string cNombreMoneda;
            //public int cListaPreciosCliente;
            //public double cDescuentoMovto;
            //public int cBanVentaCredito;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            //public string cCodigoValorClasificacionCliente1;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            //public string cCodigoValorClasificacionCliente2;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            //public string cCodigoValorClasificacionCliente3;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            //public string cCodigoValorClasificacionCliente4;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            //public string cCodigoValorClasificacionCliente5;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            public string cCodigoValorClasificacionCliente6;
            //public int cTipoCliente;
            //public int cEstatus;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongFecha)]
            //public string cFechaBaja;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongFecha)]
            //public string cFechaUltimaRevision;
            //public double cLimiteCreditoCliente;
            //public int cDiasCreditoCliente;
            //public int cBanExcederCredito;
            //public double cDescuentoProntoPago;
            //public int cDiasProntoPago;
            //public double cInteresMoratorio;
            //public int cDiaPago;
            //public int cDiasRevision;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongDesCorta)]
            //public string cMensajeria;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongDescripcion)]
            //public string cCuentaMensajeria;
            //public int cDiasEmbarqueCliente;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            //public string cCodigoAlmacen;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            //public string cCodigoAgenteVenta;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
            //public string cCodigoAgenteCobro;
            //public int cRestriccionAgente;
            //public double cImpuesto1;
            //public double cImpuesto2;
            //public double cImpuesto3;
            //public double cRetencionCliente1;
            //public double cRetencionCliente2;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            //public string cCodigoValorClasificacionProveedor1;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            //public string cCodigoValorClasificacionProveedor2;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            //public string cCodigoValorClasificacionProveedor3;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            //public string cCodigoValorClasificacionProveedor4;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            //public string cCodigoValorClasificacionProveedor5;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodValorCasif)]
            //public string cCodigoValorClasificacionProveedor6;
            //public double cLimiteCreditoProveedor;
            //public int cDiasCreditoProveedor;
            //public int cTiempoEntrega;
            //public int cDiasEmbarqueProveedor;
            //public double cImpuestoProveedor1;
            //public double cImpuestoProveedor2;
            //public double cImpuestoProveedor3;
            //public double cRetencionProveedor1;
            //public double cRetencionProveedor2;
            //public int cBanInteresMoratorio;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongTextoExtra)]
            //public string cTextoExtra1;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongTextoExtra)]
            //public string cTextoExtra2;
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongTextoExtra)]
            //public string cTextoExtra3;
            //public double cImporteExtra1;
            //public double cImporteExtra2;
            //public double cImporteExtra3;
            //public double cImporteExtra4;
        }
    }
}
