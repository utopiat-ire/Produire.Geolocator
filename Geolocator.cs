using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Produire.PGeolocator
{
	public class ジオロケーター : IProduireStaticClass
	{
		Geolocator geolocator;
		Geoposition geoposition;

		[自分で]
		public 位置情報 測位する()
		{
			if (geolocator == null) geolocator = new Geolocator();
			Task<Geoposition> t1 = null;
			Task.Run(() =>
			{
				t1 = geolocator.GetGeopositionAsync().AsTask<Geoposition>();
			}).Wait();

			try
			{
				geoposition = t1.Result;
			}
			catch (AggregateException ex)
			{
				throw ex.InnerException;
			}
			return new 位置情報(geoposition);
		}
	}
	public class 位置情報 : IProduireClass
	{
		Geoposition geoposition;
		public 位置情報(Geoposition geoposition)
		{
			this.geoposition = geoposition;
		}
		public string 計測方法
		{
			get { return geoposition.Coordinate.PositionSource.ToString(); }
		}
		public double 精度
		{
			get { return geoposition.Coordinate.Accuracy; }
		}
		public double 緯度
		{
			get { return geoposition.Coordinate.Point.Position.Latitude; }
		}
		public double 経度
		{
			get { return geoposition.Coordinate.Point.Position.Longitude; }
		}
		public double 高度
		{
			get { return geoposition.Coordinate.Point.Position.Altitude; }
		}
		public string 国
		{
			get { return (geoposition.CivicAddress != null) ? geoposition.CivicAddress.Country : null; }
		}
		public string 都道府県
		{
			get { return (geoposition.CivicAddress != null) ? geoposition.CivicAddress.State : null; }
		}
		public string 市町村
		{
			get { return (geoposition.CivicAddress != null) ? geoposition.CivicAddress.City : null; }
		}
		public string 郵便番号
		{
			get { return (geoposition.CivicAddress != null) ? geoposition.CivicAddress.PostalCode : null; }
		}
		public DateTimeOffset 測定時刻
		{
			get { return geoposition.Coordinate.Timestamp; }
		}
	}
}
