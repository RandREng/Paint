using Microsoft.AspNetCore.Mvc.Rendering;
using Paint.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Reflection;

namespace Paint.MVC.Helpers
{
    public class NullableSelectList
    {
        private class Temp<T> where T : struct
        {
            public Nullable<T> Value { get; set; }
            public string Text { get; set; }

            public Temp(Nullable<T> value, string text)
            {
                this.Value = value;
                this.Text = text;
            }
        }

        public static SelectList Create<T, Z>(IEnumerable<Z> list, string dataValue, string textValue, string nullText) where T : struct
        {
            MethodInfo v = typeof(Z).GetProperty(dataValue).GetGetMethod();
            MethodInfo t = typeof(Z).GetProperty(textValue).GetGetMethod();

            var b = list.Select(l => new Temp<T>((T) v.Invoke(l, new object[] { }), (string) t.Invoke(l, new object[] { }))).ToList();
            b.Insert(0, new Temp<T>(null, nullText));
            return new SelectList(b, "Value", "Text");
        }

        public static SelectList Create(List<Client> clients)
        {
            List<Temp<int>> temp = new List<Temp<int>>();
            temp.Add(new Temp<int>(null, "-- None --"));
            temp.AddRange(clients.Select(l => new Temp<int>(l.Id, l.Name)));
            return new SelectList(temp, "Value", "Text");
        }
    }
}
