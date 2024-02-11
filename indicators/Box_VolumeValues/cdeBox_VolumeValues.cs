#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
    public class cdeBox_Box_VolumeValues : Indicator
    {
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description               = @"cde Box shows the ""Volume Values"" in an infoBox.";
                Name                      = "cde_Box_VolumeValues";     //-- Name Drawing Objects
                Calculate                 = Calculate.OnEachTick;
                IsOverlay                 = true;
                DisplayInDataBox          = true;
                DrawOnPricePanel          = true;
                DrawHorizontalGridLines   = true;
                DrawVerticalGridLines     = true;
                PaintPriceMarkers         = true;
                ScaleJustification        = NinjaTrader.Gui.Chart.ScaleJustification.Right;

                //Disable this property if your indicator requires custom values that cumulate with each new market data event. 
                //See Help Guide for additional information.

                IsSuspendedWhileInactive  = true;
                textcolor                 = Brushes.WhiteSmoke;               

            }
            else if (State == State.Configure)
            {
            }
        }

        protected override void OnBarUpdate()
        {
            //Add your custom indicator logic here.
            double vah = 5047.25 ;
            double poc = 5042.50 ;  //--  das MUSS dynamisch !!
            double val = 5027.75 ;

            
            Draw.TextFixed( this
                          , Name   //--  refer line 34
                          , " VolumeValues ."          + "\n"      
                          + " VAH: " + vah.ToString()  + "\n"
                          + " POC: " + poc.ToString()  + "\n"
                          + " VAL: " + val.ToString()  + "  "
                          , TextPosition.TopRight
                          , textcolor      //--  ChartControl.Properties.ChartText
                          , boxTextFont    //--  ChartControl.Properties.LabelFont
                          , Brushes.OrangeRed       //--  outline 
                          , Brushes.Transparent     //--  background      
                          , 100                     //--  areaOpacity
                          , DashStyleHelper.Solid
                          , 1
                          , false     //--  bool isGlobal
                          , ""        //--  string templateName
                          );            
        }
        
        #region Properties
        [XmlIgnore]
        [Display(Name="textcolor", Order = 0, GroupName = "color Parameters")]
        public Brush textcolor
        { get; set; }        

        [Browsable(false)]
        public string textcolorSerializable
        {
            get { return Serialize.BrushToString(textcolor); }
            set { textcolor = Serialize.StringToBrush(value); }
        }        
        
        //---------
        NinjaTrader.Gui.Tools.SimpleFont boxTextFont = new NinjaTrader.Gui.Tools.SimpleFont("Courier New", 10); 
        
        #endregion
        
    }
}

#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
    public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
    {
        private cdeBox_Box_VolumeValues[] cachecdeBox_Box_VolumeValues;
        public cdeBox_Box_VolumeValues cdeBox_Box_VolumeValues()
        {
            return cdeBox_Box_VolumeValues(Input);
        }

        public cdeBox_Box_VolumeValues cdeBox_Box_VolumeValues(ISeries<double> input)
        {
            if (cachecdeBox_Box_VolumeValues != null)
                for (int idx = 0; idx < cachecdeBox_Box_VolumeValues.Length; idx++)
                    if (cachecdeBox_Box_VolumeValues[idx] != null &&  cachecdeBox_Box_VolumeValues[idx].EqualsInput(input))
                        return cachecdeBox_Box_VolumeValues[idx];
            return CacheIndicator<cdeBox_Box_VolumeValues>(new cdeBox_Box_VolumeValues(), input, ref cachecdeBox_Box_VolumeValues);
        }
    }
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
    public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
    {
        public Indicators.cdeBox_Box_VolumeValues cdeBox_Box_VolumeValues()
        {
            return indicator.cdeBox_Box_VolumeValues(Input);
        }

        public Indicators.cdeBox_Box_VolumeValues cdeBox_Box_VolumeValues(ISeries<double> input )
        {
            return indicator.cdeBox_Box_VolumeValues(input);
        }
    }
}

namespace NinjaTrader.NinjaScript.Strategies
{
    public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
    {
        public Indicators.cdeBox_Box_VolumeValues cdeBox_Box_VolumeValues()
        {
            return indicator.cdeBox_Box_VolumeValues(Input);
        }

        public Indicators.cdeBox_Box_VolumeValues cdeBox_Box_VolumeValues(ISeries<double> input )
        {
            return indicator.cdeBox_Box_VolumeValues(input);
        }
    }
}

#endregion
