using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Day3
{
    internal class SchematicReader
    {
        internal Schematic Read(string[] lines)
        {
            Schematic schematic = new()
            {
                ItemGrid = new IEngineItem[lines.Length, lines[0].Length]
            };
            
            for(int i = 0; i < lines.Length; i++)
            {
                // Numbers can span multiple grid items,
                // so hold onto the current one so we can 
                // use a single object for each grid item
                // that makes up a number
                var currentNumber = new Number();

                for (int j = 0; j < lines[i].Length; j++)
                {
                    var currentState = lines[i][j];
                    var itemType = GetItemType(currentState);

                    if (itemType == SchematicItem.Empty)
                    {
                        schematic.ItemGrid[i, j] = new Empty();
                        currentNumber = new Number();
                    }
                    else if (itemType == SchematicItem.Symbol)
                    {
                        schematic.ItemGrid[i, j] = new Symbol();
                        currentNumber = new Number();
                    }
                    else
                    {
                        currentNumber.AddDigit(currentState);
                        schematic.ItemGrid[i, j] = currentNumber;
                    }

                    schematic.Items.Add(schematic.ItemGrid[i, j]);
                }
            }

            // Numbers are duplicated; remove them.
            schematic.Items = schematic.Items.Distinct().ToList();
            return schematic;
        }

        internal SchematicItem GetItemType(char c)
        {
            if (char.IsDigit(c))
            {
                return SchematicItem.Number;
            }
            
            if (c == '*')
            {
                return SchematicItem.Symbol;
            }

            return SchematicItem.Empty;
        }
    }

    internal enum SchematicItem
    {
        Empty = 1,
        Number,
        Symbol
    }

    internal class Schematic
    {
        public IEngineItem[,] ItemGrid { get; set; }

        public List<IEngineItem> Items { get; set; } = new();
    }

    internal interface IEngineItem
    {

    }

    internal class Number : IEngineItem
    {
        public int Value { get; set; }

        public void AddDigit(char c)
        {
            Value = Value * 10 + int.Parse(c.ToString());
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    internal class Empty : IEngineItem
    {
        public override string ToString()
        {
            return "Empty";
        }
    }

    internal class Symbol : IEngineItem
    {
        public List<Number> Neighbours { get; set; } = new();

        public override string ToString()
        {
            return $"Symbol {Neighbours.Count}";
        }
    }
}
