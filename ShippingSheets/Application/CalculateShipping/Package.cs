using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShippingSheets.Application.CalculateShipping
{
    public class Package
    {
        private const int minLength = 16;
        private const int minWidth = 11;
        private const int minHeight = 2;

        private double weight;
        private double length;
        private double width;
        private double height;
        private readonly List<PackageItem> items = new List<PackageItem>();

        public Package() { }

        public Package(IEnumerable<PackageItem> items)
        {
            foreach (var item in items) AddItem(item);
        }

        public int Weight => (int)Math.Ceiling(weight);
        public double Volume { get; private set; }
        public int PriceCents => items.Sum(item => item.PriceCents);
        public ReadOnlyCollection<PackageItem> Items => items.AsReadOnly();

        public Package AddItem(PackageItem item)
        {
            items.Add(item);
            UpdateMeasures(item);
            return this;
        }

        private void UpdateMeasures(PackageItem item)
        {
            weight += item.Weight;
            Volume += item.Volume;

            if (Items.Count == 1)
            {
                length = item.Length;
                width = item.Width;
                height = item.Height;
            }
            else
            {
                var dimension = Math.Pow(Volume, 1d / 3);
                length = width = height = dimension;
            }

            EnsureMinMeasures();
        }

        private void EnsureMinMeasures()
        {
            length = Math.Max(length, minLength);
            width = Math.Max(width, minWidth);
            height = Math.Max(height, minHeight);
        }
    }

    public class PackageItem
    {
        public PackageItem(int priceCents, int weightGrams, int lengthCentimeters, int widthCentimeters, int heightCentimeters)
        {
            PriceCents = priceCents;
            Weight = weightGrams;
            Length = lengthCentimeters;
            Width = widthCentimeters;
            Height = heightCentimeters;
        }

        public int PriceCents { get; }
        public int Weight { get; }
        public int Length { get; }
        public int Width { get; }
        public int Height { get; }
        public double Volume => Length * Height * Width;
    }
}
