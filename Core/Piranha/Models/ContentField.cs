using Newtonsoft.Json;
using System;
using System.Linq;

namespace Piranha.Models
{
	public sealed class ContentField : Data.IModel
	{
		#region Properties
		public Guid Id { get; set; }
		public Guid ContentId { get; set; }
		public Guid TemplateFieldId { get; set; }
		public string CLRType { get; set; }
		public string Value { get; set; }
		#endregion

		#region Navigation properties
		public Content Content { get; set; }
		public TemplateField TemplateField { get; set; }
		#endregion

		/// <summary>
		/// Gets/sets the deserialized body.
		/// </summary>
		public Extend.IComponent Body {
			get {
				var type = App.Extensions.TemplateFields
					.Where(r => r.ValueType.FullName == CLRType)
					.Select(r => r.ValueType)
					.SingleOrDefault();
				if (type != null) {
					var serializer = App.Serializers[type];

					if (serializer != null)
						return (Extend.IComponent)serializer.Deserialize(Value);
					return (Extend.IComponent)JsonConvert.DeserializeObject(Value, type);
				} else {
					App.Logger.Log(Log.LogLevel.ERROR, "ContentField.Body: Deserialization error. Couldn't find CLRType " + CLRType);
				}
				return null;
			}
			set {
				var type = App.Extensions.TemplateFields
					.Where(r => r.ValueType.FullName == CLRType)
					.Select(r => r.ValueType)
					.SingleOrDefault();
				if (type != null) {
					var serializer = App.Serializers[type];
					Value = serializer != null ? serializer.Serialize(value) : JsonConvert.SerializeObject(value);
				} else {
					App.Logger.Log(Log.LogLevel.ERROR, "ContentField.Body: Serialization error. Couldn't find CLRType " + CLRType);
				}
			}
		}
	}
}
