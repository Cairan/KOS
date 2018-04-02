using kOS.Safe.Encapsulation;
using kOS.Safe.Encapsulation.Suffixes;
using System.Collections.Generic;

namespace kOS.Suffixed.Part
{
    [kOS.Safe.Utilities.KOSNomenclature("Decoupler")]
    [kOS.Safe.Utilities.KOSNomenclature("Separator", CSharpToKOS = false)]
    public class DecouplerValue : PartValue
    {
        /// <summary>
        /// Do not call! VesselTarget.ConstructPart uses this, would use `friend VesselTarget` if this was C++!
        /// </summary>
        internal DecouplerValue(SharedObjects shared, global::Part part, PartValue parent, DecouplerValue decoupler)
            : base(shared, part, parent, decoupler)
        {
        }

        public static ListValue PartsToList(IEnumerable<global::Part> parts, SharedObjects sharedObj)
        {
            var toReturn = new ListValue();
            var vessel = VesselTarget.CreateOrGetExisting(sharedObj);
            foreach (var part in parts)
            {
                foreach (PartModule module in part.Modules)
                {
                    if (!(module is IStageSeparator))
                        continue;
                    if (module is ModuleDockingNode dockingNode && !dockingNode.stagingEnabled)
                        continue;
                    if (module is ModuleDecouple || module is ModuleAnchoredDecoupler
                        || module is LaunchClamp)
                    {
                        toReturn.Add(vessel[part]);
                    }
                }
            }
            return toReturn;
        }
    }
}
