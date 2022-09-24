using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("eventHandlerComponentsAdded", "Fsm")]
	public class ES3UserType_PlayMakerFSM : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_PlayMakerFSM() : base(typeof(PlayMakerFSM)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (PlayMakerFSM)obj;
			
			writer.WritePrivateField("eventHandlerComponentsAdded", instance);
			writer.WriteProperty("Fsm", instance.Fsm, ES3Type_Fsm.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (PlayMakerFSM)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "eventHandlerComponentsAdded":
					reader.SetPrivateField("eventHandlerComponentsAdded", reader.Read<System.Boolean>(), instance);
					break;
					case "Fsm":
						instance.Fsm = reader.Read<HutongGames.PlayMaker.Fsm>(ES3Type_Fsm.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_PlayMakerFSMArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayMakerFSMArray() : base(typeof(PlayMakerFSM[]), ES3UserType_PlayMakerFSM.Instance)
		{
			Instance = this;
		}
	}
}