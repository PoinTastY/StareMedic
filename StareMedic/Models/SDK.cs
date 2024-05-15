﻿using System;
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
    }
}
