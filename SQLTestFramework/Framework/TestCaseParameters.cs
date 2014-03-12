using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTestFramework.Framework
{
    /// <summary>
    /// Class representing internal (not-user defined) parameters that may be useful during test runs.
    /// </summary>
    [Serializable]
    public class TestCaseParameters
    {
        public int Identifier { get; set; }
        public Nullable<Boolean> UsesBisonParser { get; set; }
        public Nullable<Boolean> UsesOrderBy { get; set; }
        public Nullable<Boolean> SingleObjectProjection { get; set; }
        public Nullable<Boolean> ContainsLiterals { get; set; }

        public TestCaseParameters(int id)
        {
            Identifier = id;
            UsesOrderBy = null;
            UsesBisonParser = null;
            SingleObjectProjection = null;
            ContainsLiterals = null;
        }

        public String ToString()
        {
            return Environment.NewLine + "Identifier: " + Identifier +
                Environment.NewLine + "UsesOrderBy: " + UsesOrderBy +
                Environment.NewLine + "UsesBisonParser: " + UsesBisonParser +
                Environment.NewLine + "SingleObjectProjection: " + SingleObjectProjection +
                Environment.NewLine + "ContainsLiteral: " + ContainsLiterals + Environment.NewLine;
        }
    }
}