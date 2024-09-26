namespace StareMedic.Models.DTOs
{
    public class DocumentDTO
    {
        //weas del struct documento:
        public double aFolio { get; set; }
        public int aNumMoneda { get; set; }
        public double aTipoCambio { get; set; }
        public double aImporte { get; set; }
        public double aDescuentoDoc1 { get; set; }
        public double aDescuentoDoc2 { get; set; }
        public int aSistemaOrigen { get; set; }
        public string aCodConcepto { get; set; } = string.Empty;
        public string aSerie { get; set; } = string.Empty;
        public string aFecha { get; set; } = string.Empty;
        public string aCodigoCteProv { get; set; } = string.Empty;
        public string aCodigoAgente { get; set; } = string.Empty;
        public string aReferencia { get; set; } = string.Empty;
        public int aAfecta { get; set; }
        public double aGasto1 { get; set; }
        public double aGasto2 { get; set; }
        public double aGasto3 { get; set; }

        //weas del struct movimiento:
        public int aConsecutivo { get; set; }
        public double aUnidades { get; set; }
        public double aPrecio { get; set; }
        public double aCosto { get; set; }
        public string aCodProdSer { get; set; } = string.Empty;
        public string aCodAlmacen { get; set; } = string.Empty;
        public string aReferenciaMov { get; set; } = string.Empty;
        public string aCodClasificacion { get; set; } = string.Empty;

        public int CIDDOCUMENTO { get; set; }
        public DateTime CFECHA { get; set; }
        public string CRAZONSOCIAL { get; set; } = string.Empty;
        public double CTOTAL { get; set; }
        public string COBSERVACIONES { get; set; } = string.Empty;
        public string CTEXTOEXTRA1 { get; set; } = string.Empty;
        public string CTEXTOEXTRA2 { get; set; } = string.Empty;
        public string CTEXTOEXTRA3 { get; set; } = string.Empty;
        public int CIMPRESO { get; set; }
    }
}
