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
using NinjaTrader.NinjaScript.Indicators;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

//This namespace holds Strategies in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Strategies
{
    public class cde_sunny01 : Strategy
    {
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description                    = @"v1.06 // example from sunny (3.9)  /  Volume and ATR";
                Name                           = "cde_sunny01";
                Calculate                      = Calculate.OnBarClose;
                EntriesPerDirection            = 1;
                EntryHandling                  = EntryHandling.AllEntries;
                IsExitOnSessionCloseStrategy   = true;
                ExitOnSessionCloseSeconds      = 30;
                IsFillLimitOnTouch             = false;
                MaximumBarsLookBack            = MaximumBarsLookBack.TwoHundredFiftySix;
                OrderFillResolution            = OrderFillResolution.Standard;
                Slippage                       = 0;
                StartBehavior                  = StartBehavior.WaitUntilFlat;
                TimeInForce                    = TimeInForce.Gtc;
                TraceOrders                    = false;
                RealtimeErrorHandling          = RealtimeErrorHandling.IgnoreAllErrors;  //-- original RealtimeErrorHandling.StopCancelClose;
                StopTargetHandling             = StopTargetHandling.PerEntryExecution;
                BarsRequiredToTrade            = 20;

                PrintTo                        = PrintTo.OutputTab1;

                // Disable this property for performance gains in Strategy Analyzer optimizations
                // See the Help Guide for additional information
                IsInstantiatedOnEachOptimizationIteration    = true;

                //--  FourEMAs configValues
                EMA1            =   5 ;
                EMA2            =  10 ;
                EMA3            =  20 ;
                EMA4            = 100 ;

                RSIupperL       =  70 ;
                RSIlowerL       =  30 ;
                RSIperiod       =  14 ;
                RSIsmooth       =   3 ;
                LookBack        =   8 ;

                //--  Trade config with TP
                Quantity        =     1 ;
                PT_ticks        =   800 ;  //-- PT = ProfitTarget
                SL_ticks        =    16 ;  //-- SL = StopLoss
                SL_isTrailing   =  true ;

                Long_desc       = @"Long_Entry_v1.06";
                Short_desc      = @"Short_Entry_v1.06";
                StartTime       = DateTime.Parse("15:30", System.Globalization.CultureInfo.InvariantCulture);
                EndTime         = DateTime.Parse("21:40", System.Globalization.CultureInfo.InvariantCulture);

                AddPlot(new Stroke(Brushes.OrangeRed, 2), PlotStyle.TriangleUp  , "Long_Entry_plot" );
                AddPlot(new Stroke(Brushes.OrangeRed, 2), PlotStyle.TriangleDown, "Short_Entry_plot");
            }
            else if (State == State.Configure)
            {
                //-- Long
                SetProfitTarget(Long_desc, CalculationMode.Ticks, PT_ticks);
                if ( SL_isTrailing ) //-- default = false
                   SetStopLoss( Long_desc, CalculationMode.Ticks, SL_ticks, false);
                else
                   SetTrailStop(Long_desc, CalculationMode.Ticks, SL_ticks, false);  //-- true | false --> isMIT : Sets the StopLoss  as a "market-if-touched" order
                
                //-- Short
                SetProfitTarget(Short_desc, CalculationMode.Ticks, PT_ticks);
                if ( SL_isTrailing ) //-- default = false                
                   SetStopLoss( Short_desc, CalculationMode.Ticks, SL_ticks, false);
                else
                   SetTrailStop(Short_desc, CalculationMode.Ticks, SL_ticks, false);
            }
        }

        protected override void OnBarUpdate()
        {
            //Add your custom strategy logic here.
            bool noTrades = Position.MarketPosition == MarketPosition.Flat;  //-- enter Long/Short only from "flat"-Status
            
            if (CurrentBar < 100) return;  //-- too less bars --> return;
            
//            if (   (Times[0][0].TimeOfDay < auto_StartTime.TimeOfDay)  //-- only on "Main" TradingHours
//                || (Times[0][0].TimeOfDay > auto_EndTime.TimeOfDay)
//               )       return;        
            
            #region RSIcalculation
            
            double minRSI =   0 ;
            double maxRSI =   0 ;
            string sigRSI = "-" ;  //-- RSI Signal [ - | L | S ]  //--  flat | Long | Short
            
            // minRSI = MIN( RSI( RSIperiod, RSIsmooth ), 8 )[0] ;  //-- static
            minRSI = MIN( RSI( RSIperiod, RSIsmooth ), LookBack )[0] ;  
            maxRSI = MAX( RSI( RSIperiod, RSIsmooth ), LookBack )[0] ;  
            
            //Print( Times[0][0].TimeOfDay.ToString() + "__flat" );
            Print( string.Format( "RSI || {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} || {8} | {9}"
                   , String.Format("{0:0.00}", RSI( RSIperiod, RSIsmooth )[0])
                   , String.Format("{0:0.00}", RSI( RSIperiod, RSIsmooth )[1])
                   , String.Format("{0:0.00}", RSI( RSIperiod, RSIsmooth )[2])
                   , String.Format("{0:0.00}", RSI( RSIperiod, RSIsmooth )[3])
                   , String.Format("{0:0.00}", RSI( RSIperiod, RSIsmooth )[4])
                   , String.Format("{0:0.00}", RSI( RSIperiod, RSIsmooth )[5])
                   , String.Format("{0:0.00}", RSI( RSIperiod, RSIsmooth )[6])
                   , String.Format("{0:0.00}", RSI( RSIperiod, RSIsmooth )[7])
                   , String.Format("{0:0.00}", minRSI)
                   , String.Format("{0:0.00}", maxRSI)
                 ) );
            
            #endregion

            //---------------------------------------------------------------------

            #region EnterTrades
            //--conditions "Long"
            if (    noTrades  
                &&  CrossAbove( EMA(EMA1), EMA(EMA2), 1)
                //&&  RSI( RSIperiod, RSIsmooth )[0] <= RSIupperL   // RSI(period, smooth)[0]   //-- period = Number of bars used in the calculation, smooth = smoothing period
                ) {
                EnterLong( Quantity, Long_desc );
                Print(Times[0][0].TimeOfDay.ToString() + "__long");
                //--Draw.ArrowUp(this, @"arrowUp_info", false, 0, 0, Brushes.Green);
                }    

            //-- conditions "Short"
            if (    noTrades  
                &&  CrossBelow( EMA(EMA1), EMA(EMA2), 1)
                //&&  RSI( RSIperiod, RSIsmooth )[0] >= RSIlowerL
               ) {
                EnterShort( Quantity, Short_desc );
                Print(Times[0][0].TimeOfDay.ToString() + "__shot");
                //--Draw.ArrowDown(this, @"arrowDown_info", false, 0, 0, Brushes.Red);
               }   
            #endregion
               
        } //--  end OnBarUpdate

        #region Properties
        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="EMA1", Order=11, GroupName="1Parameters")]
        public int EMA1
        { get; set; }  

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="EMA2", Order=12, GroupName="1Parameters")]
        public int EMA2
        { get; set; }  

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="EMA3", Order=13, GroupName="1Parameters")]
        public int EMA3
        { get; set; }  

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="EMA4", Order=14, GroupName="1Parameters")]
        public int EMA4
        { get; set; }  

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="RSIupperL", Order=21, GroupName="2Parameters")]
        public int RSIupperL
        { get; set; }       

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="RSIlowerL", Order=22, GroupName="2Parameters")]
        public int RSIlowerL
        { get; set; }       

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="RSIperiod", Order=23, GroupName="2Parameters")]
        public int RSIperiod
        { get; set; }       

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="RSIsmooth", Order=24, GroupName="2Parameters")]
        public int RSIsmooth
        { get; set; }       

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="LookBack", Order=25, GroupName="2Parameters")]
        public int LookBack
        { get; set; }      

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="Quantity", Order=31, GroupName="3Parameters")]
        public int Quantity
        { get; set; }      

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="PT_ticks", Order=32, GroupName="3Parameters")]
        public int PT_ticks
        { get; set; }      

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name="SL_ticks", Order=33, GroupName="3Parameters")]
        public int SL_ticks
        { get; set; }      

        [NinjaScriptProperty]
        [Display(Name="SL_isTrailing", Order=34, GroupName="3Parameters")]
        public bool SL_isTrailing
        { get; set; }            

        [NinjaScriptProperty]
        [Display(Name="Long_desc", Order=35, GroupName="3Parameters")]
        public string Long_desc
        { get; set; }          

        [NinjaScriptProperty]
        [Display(Name="Short_desc", Order=36, GroupName="3Parameters")]
        public string Short_desc
        { get; set; }           

        [NinjaScriptProperty]
        [PropertyEditor("NinjaTrader.Gui.Tools.TimeEditorKey")]
        [Display(Name="StartTime", Order=11, GroupName="Parameters")]
        public DateTime StartTime
        { get; set; }            

        [NinjaScriptProperty]
        [PropertyEditor("NinjaTrader.Gui.Tools.TimeEditorKey")]
        [Display(Name="EndTime", Order=12, GroupName="Parameters")]
        public DateTime EndTime
        { get; set; }          

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> Long_Entry_plot
        {
            get { return Values[0]; }
        }

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> Short_Entry_plot
        {
            get { return Values[1]; }
        }
        #endregion

    }
}
