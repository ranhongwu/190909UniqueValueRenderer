using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Display;
using System.Runtime.InteropServices;
using System.Collections;

namespace UniqueValueRenderer
{
    public partial class FrmUniqueValueRenderer : DevExpress.XtraEditors.XtraForm
    {
        public FrmUniqueValueRenderer(IMap map)
        {
            InitializeComponent();
            pMap = map;
        }

        #region 定义变量
        private IMap pMap = null;
        IFeatureClass pFeatureClass = null;
        IFeatureLayer pFeatureLayer = new FeatureLayer();
        #endregion

        //加载窗体事件
        private void FrmUniqueValueRenderer_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                if (pMap.Layer[i] is FeatureLayer)
                {
                    cbxLayers.Properties.Items.Add(pMap.Layer[i].Name);
                }
            }
        }

        //选中图层后将属性字段加入属性下拉框
        private void cbxLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> attList = new List<string>();
            cbxFields.Properties.Items.Clear();
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                if ((pMap.Layer[i] is FeatureLayer) && (pMap.Layer[i].Name == cbxLayers.Text))
                {
                    pFeatureLayer = pMap.Layer[i] as IFeatureLayer;
                    pFeatureClass = pFeatureLayer.FeatureClass;
                }
            }
            attList = get_FieldsString(pFeatureClass);
            foreach (string s in attList)
            {
                cbxFields.Properties.Items.Add(s);
            }
        }

        //确定按钮事件
        private void btnOK_Click(object sender, EventArgs e)
        {
            string fieldName = cbxFields.Text;
            UniqueValueRenderer(pFeatureLayer, fieldName);
            IActiveView pActiveView = pMap as IActiveView;
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
        }

        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region 封装方法
        /// <summary>
        /// 获取待要素类的所有属性字段名
        /// </summary>
        /// <param name="pFeatureClass">待复制要素类</param>
        /// <returns>返回待复制要素类的所有属性字段名</returns>
        public List<string> get_FieldsString(IFeatureClass pFeatureClass)
        {
            IFields pFields = pFeatureClass.Fields;
            IField pField;
            List<string> s = new List<string>();
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                pField = pFields.Field[i];
                if (pField.Type != esriFieldType.esriFieldTypeGeometry)
                    s.Add(pField.Name);
            }
            return s;
        }

        /// <summary>
        /// 实现要素图层属性唯一值设色
        /// </summary>
        /// <param name="pFeatureLayer">需要设色的要素图层</param>
        /// <param name="fieldName">唯一值的属性字段</param>
        private void UniqueValueRenderer(IFeatureLayer pFeatureLayer, string fieldName)
        {
            IGeoFeatureLayer pGeoFeatureLayer = pFeatureLayer as IGeoFeatureLayer;
            ITable pTable = pFeatureLayer as ITable;
            int fieldNumber = pTable.FindField(fieldName);
            IUniqueValueRenderer pUniqueRenderer = new UniqueValueRendererClass();
            pUniqueRenderer.FieldCount = 1;
            pUniqueRenderer.set_Field(0, fieldName);

            //设置显示颜色的范围
            IRandomColorRamp pRandColorRamp = new RandomColorRampClass();
            pRandColorRamp.StartHue = 0;
            pRandColorRamp.MinValue = 85;
            pRandColorRamp.MinSaturation = 15;
            pRandColorRamp.EndHue = 360;
            pRandColorRamp.MaxValue = 100;
            pRandColorRamp.MaxSaturation = 30;

            //创建随机颜色带
            pRandColorRamp.Size = getUniqueValue(pFeatureLayer.FeatureClass, fieldName).Count;
            bool bSucess = false;
            pRandColorRamp.CreateRamp(out bSucess);

            IEnumColors pEnumColors = pRandColorRamp.Colors;
            IColor pNextUniqueColor = null;

            //属性唯一值
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.AddField(fieldName);
            ICursor pCursor = pFeatureLayer.FeatureClass.Search(pQueryFilter, false) as ICursor;
            IRow pNextRow = pCursor.NextRow();
            object codeValue = null;
            IRowBuffer pNextRowBuffer = null;

            IFillSymbol pFillSymbol = null;
            ILineSymbol pLineSymbol = null;
            IMarkerSymbol pMarkerSymbol = null;
            while (pNextRow != null)
            {
                pNextRowBuffer = pNextRow as IRowBuffer;
                codeValue = pNextRowBuffer.get_Value(fieldNumber);
                pNextUniqueColor = pEnumColors.Next();
                if (pNextUniqueColor == null)
                {
                    pEnumColors.Reset();
                    pNextUniqueColor = pEnumColors.Next();
                }
                switch (pGeoFeatureLayer.FeatureClass.ShapeType)
                {
                    case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                        pMarkerSymbol = new SimpleMarkerSymbolClass();
                        pMarkerSymbol.Color = pNextUniqueColor;
                        pUniqueRenderer.AddValue(codeValue.ToString(), "", pMarkerSymbol as ISymbol);
                        pNextRow = pCursor.NextRow();
                        break;
                    case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                        pLineSymbol = new SimpleLineSymbolClass();
                        pLineSymbol.Color = pNextUniqueColor;
                        pUniqueRenderer.AddValue(codeValue.ToString(), "", pLineSymbol as ISymbol);
                        pNextRow = pCursor.NextRow();
                        break;
                    case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                        pFillSymbol = new SimpleFillSymbolClass();
                        pFillSymbol.Color = pNextUniqueColor;
                        pUniqueRenderer.AddValue(codeValue.ToString(), "", pFillSymbol as ISymbol);
                        pNextRow = pCursor.NextRow();
                        int k = pFillSymbol.Color.CMYK;
                        break;
                    default:
                        break;
                }
            }
            pGeoFeatureLayer.Renderer = pUniqueRenderer as IFeatureRenderer;

            //必须手动清除COM对象，否则会造成内存溢出（尤其是IQueryFilter,ICursor）
            Marshal.ReleaseComObject(pQueryFilter);
            Marshal.ReleaseComObject(pCursor);
            Marshal.ReleaseComObject(pEnumColors);
        }

        /// <summary>
        /// 查找某一属性的唯一值，返回属性唯一值的列表
        /// </summary>
        /// <param name="pFeatureClass">待查找的FeatureClass</param>
        /// <param name="field">待查找唯一值的字段</param>
        /// <returns>返回字段唯一值字符串的唯一值列表</returns>
        public static List<object> getUniqueValue(IFeatureClass pFeatureClass, string field)
        {
            string s = pFeatureClass.AliasName;
            List<object> uniqueValue = new List<object>();
            IFeatureCursor pCursor = pFeatureClass.Search(null, false);
            IDataStatistics pDataStat = new DataStatisticsClass();
            pDataStat.Field = field;
            pDataStat.Cursor = pCursor as ICursor;
            IEnumerator pEnumerator = pDataStat.UniqueValues;
            pEnumerator.Reset();
            while (pEnumerator.MoveNext())
            {
                uniqueValue.Add(pEnumerator.Current.ToString());
            }
            return uniqueValue;
        }
        #endregion
        
    }
}