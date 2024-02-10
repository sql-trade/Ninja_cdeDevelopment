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

#endregion



#region NinjaScript generated code. Neither change nor remove.

namespace NinjaTrader.NinjaScript.Indicators
{
	public partial class Indicator : NinjaTrader.Gui.NinjaScript.IndicatorRenderBase
	{
		
		private cdeATRbox[] cachecdeATRbox;

		
		public cdeATRbox cdeATRbox(int period, double atrThreshold, double adxThreshold)
		{
			return cdeATRbox(Input, period, atrThreshold, adxThreshold);
		}


		
		public cdeATRbox cdeATRbox(ISeries<double> input, int period, double atrThreshold, double adxThreshold)
		{
			if (cachecdeATRbox != null)
				for (int idx = 0; idx < cachecdeATRbox.Length; idx++)
					if (cachecdeATRbox[idx].Period == period && cachecdeATRbox[idx].atrThreshold == atrThreshold && cachecdeATRbox[idx].adxThreshold == adxThreshold && cachecdeATRbox[idx].EqualsInput(input))
						return cachecdeATRbox[idx];
			return CacheIndicator<cdeATRbox>(new cdeATRbox(){ Period = period, atrThreshold = atrThreshold, adxThreshold = adxThreshold }, input, ref cachecdeATRbox);
		}

	}
}

namespace NinjaTrader.NinjaScript.MarketAnalyzerColumns
{
	public partial class MarketAnalyzerColumn : MarketAnalyzerColumnBase
	{
		
		public Indicators.cdeATRbox cdeATRbox(int period, double atrThreshold, double adxThreshold)
		{
			return indicator.cdeATRbox(Input, period, atrThreshold, adxThreshold);
		}


		
		public Indicators.cdeATRbox cdeATRbox(ISeries<double> input , int period, double atrThreshold, double adxThreshold)
		{
			return indicator.cdeATRbox(input, period, atrThreshold, adxThreshold);
		}
	
	}
}

namespace NinjaTrader.NinjaScript.Strategies
{
	public partial class Strategy : NinjaTrader.Gui.NinjaScript.StrategyRenderBase
	{
		
		public Indicators.cdeATRbox cdeATRbox(int period, double atrThreshold, double adxThreshold)
		{
			return indicator.cdeATRbox(Input, period, atrThreshold, adxThreshold);
		}


		
		public Indicators.cdeATRbox cdeATRbox(ISeries<double> input , int period, double atrThreshold, double adxThreshold)
		{
			return indicator.cdeATRbox(input, period, atrThreshold, adxThreshold);
		}

	}
}

#endregion
