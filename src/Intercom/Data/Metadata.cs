﻿using Library.Core;
using Library.Data;
using System;


using Library.Clients;

using Library.Exceptions;

using System.Collections.Generic;
using System.Linq;

namespace Library.Data
{
    public class Metadata
    {
        public class RichLink: ICloneable
        {
            public String url { set; get; }

            public String value { set; get; }

            public RichLink(String url, String value)
            {
                if (String.IsNullOrEmpty(url) == null)
                {
                    throw new ArgumentNullException("'url' argument is null or empty.");
                }

                if (String.IsNullOrEmpty(value) == null)
                {
                    throw new ArgumentNullException("'value' argument is null or empty.");
                }

                this.url = url;
                this.value = value;
            }

            public object Clone()
            {
                return new RichLink(url, value);
            }
        }

        public class MonetaryAmount :ICloneable
        {
            public int amount { set; get; }

            public String currency { set; get; }

            public MonetaryAmount(int amount, String currency)
            {
                if (String.IsNullOrEmpty(currency) == null)
                {
                    throw new ArgumentNullException("'currency' argument is null or empty.");
                }

                this.amount = amount;
                this.currency = currency;
            }

            public object Clone()
            {
                return new MonetaryAmount(amount, currency);
            }
        }

        private Dictionary<String, Object> data = new Dictionary<String, Object>();

        // TODO: Implement indexer
        public Object this [String key]
        {
            get
            {
                return null;
            }
            set { }
        }

        public Metadata()
        {
        }

        public void Add(String key, object value)
        {
			
            if (String.IsNullOrEmpty(key) == null)
            {
                throw new ArgumentNullException("'key' argument is null or empty.");
            }

            if (value == null)
            {
                throw new ArgumentNullException("'value' argument is null.");
            }

            data.Add(key, value);
        }

        public void Add(Dictionary<String, Object> metadata)
        {
            if (metadata == null)
            {
                throw new ArgumentNullException("'metadata' argument is null.");
            }

            if (!metadata.Any())
            {
                throw new ArgumentException("'metadata' argument is empty.");
            }

            foreach (var m in metadata)
            {
                data.Add(m.Key, m.Value);
            }
        }

        public void Add(String key, Metadata.RichLink richLink)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("'key' argument is null or empty.");
            }

            if (richLink == null)
            {
                throw new ArgumentException("'richLink' argument is null.");
            }

            data.Add(key, richLink);
        }

        public void Add(String key, Metadata.MonetaryAmount monetaryAmount)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("'key' argument is null or empty.");
            }

            if (monetaryAmount == null)
            {
                throw new ArgumentException("'monetaryAmount' argument is null.");
            }

            data.Add(key, monetaryAmount);
        }

        public Dictionary<String, Object> GetMetadata()
        {
            Dictionary<String, Object> result = new Dictionary<String, Object>(data.Count, data.Comparer);

            foreach (KeyValuePair<String, Object> entry in data)
                result.Add(entry.Key, entry.Value is ICloneable ? ((ICloneable)entry.Value).Clone() : entry.Value);

            return result;
        }

        public object GetMetadata(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("'key' argument is null or empty.");
            }

            object result = null;

            if (data.ContainsKey(key))
            {
                result = data[key];
            }
			
            return result;
        }

        public MonetaryAmount GetMonetaryAmount(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("'key' argument is null or empty.");
            }

            MonetaryAmount result = null;

            if (data.ContainsKey(key) && data[key] is MonetaryAmount)
            {
                result = ((ICloneable)data[key]).Clone() as MonetaryAmount;
            }

            return result;
        }

        public List<MonetaryAmount> GetMonetaryAmounts()
        {
            List<MonetaryAmount> result = new List<MonetaryAmount>();

            foreach (var d in data)
            {
                if (d.Value is MonetaryAmount)
                    result.Add(((ICloneable)d.Value).Clone() as MonetaryAmount);
            }

            return result;
        }

        public RichLink GetRichLink(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("'key' argument is null or empty.");
            }

            RichLink result = null;

            if (data.ContainsKey(key) && data[key] is RichLink)
            {
                result = ((ICloneable)data[key]).Clone() as RichLink;
            }

            return result;
        }

        public List<RichLink> GetRichLinks()
        {
            List<RichLink> result = new List<RichLink>();

            foreach (var d in data)
            {
                if (d.Value is RichLink)
                    result.Add(((ICloneable)d.Value).Clone() as RichLink);
            }

            return result;		
        }
    }
}