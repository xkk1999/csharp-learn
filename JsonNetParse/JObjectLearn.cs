﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace JsonNetParse
{
    /// <summary>
    /// JSON can be read and written using JObject, JArray, JValue, JToken.
    /// Called "LINQ to JSON" in the docs.
    /// </summary>
    public class JObjectLearn
    {
        public static void Run()
        {
            var jsonString = "{astring: 'foo', anumber: 12, " +
                "anobject: {foo: 1, bar: 'abc', spam: {'a': 1}}, " +
                "anarray: [1, 2, 3], aboolean: true, anull: null}";

            // Parse as JSON object (can also be an array or dynamic type).
            var jobject = JObject.Parse(jsonString);
            
            // Iterate JSON object and print values.
            foreach (var item in jobject)
            {
                var key = item.Key;
                var value = item.Value;
                Console.WriteLine($"> {key}: {value} ({value.GetType()})");
            }

            // Get JToken value and cast as concrete type.
            bool aboolean = jobject.GetValue("aboolean").Value<bool>();
            Console.WriteLine(aboolean);

            // Get string field or null.
            var something = jobject.GetValue("nonexistant")?.Value<string>();
            Console.WriteLine($"Something: '{something}'");

            // Get JObject property as IDictionary.
            var anobject = jobject.GetValue("anobject").ToObject<IDictionary<string, object>>();
            Console.WriteLine(anobject.GetType());
            foreach (var entry in anobject)
            {
                Console.WriteLine($"  {entry.Key}: {entry.Value}");
            }

            // Try to get non-existant.
            var nonexistant = (JObject) jobject["nonexistant"];
            Console.WriteLine($"nonexistant: {nonexistant}");

            var obj = (JObject) jobject["anobject"];
            Console.WriteLine($"obj: {obj}");
            foreach (var p in obj)
            {
                var key = p.Key;
                var value = p.Value;
                Console.WriteLine($"  obj.prop: {key}: {value}");
            }

            // Dynamic feature.
            dynamic ob = jobject["anobject"];
            int obFoo = ob.foo;
            string obBar = ob.bar;
            Console.WriteLine($"{obFoo}, {obBar}");

            // Newtonsoft json parser tries to convert to a type if possible.
            var anumberStr = jobject.Value<string>("anumber");
            Console.WriteLine("anumber as string: {0}", anumberStr);
        }
    }
}
