﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace Rawr.Bear
{
#if !SILVERLIGHT
	[Serializable]
#endif
	public class CalculationOptionsBear : ICalculationOptionBase, INotifyPropertyChanged
	{
		public string GetXml()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(CalculationOptionsBear));
			StringBuilder xml = new StringBuilder();
			System.IO.StringWriter writer = new System.IO.StringWriter(xml);
			serializer.Serialize(writer, this);
			return xml.ToString();
		}

		#region Rating Customization
		private float _threatScale = 5f;
		public float ThreatScale
		{
			get { return _threatScale; }
			set { if (_threatScale != value) { _threatScale = value; OnPropertyChanged("ThreatScale"); } }
		}
		private double _hitsToSurvive = 3.5f;
        public double HitsToSurvive
		{
			get { return _hitsToSurvive; }
            set { if (_hitsToSurvive != value) { _hitsToSurvive = value; OnPropertyChanged("HitsToSurvive"); } }
		}
		private float _temporarySurvivalScale = 1f;
		public float TemporarySurvivalScale
		{
			get { return _temporarySurvivalScale; }
			set { if (_temporarySurvivalScale != value) { _temporarySurvivalScale = value; OnPropertyChanged("TemporarySurvivalScale"); } }
		}
		#endregion

		#region INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); }
		}
		#endregion
	}
}