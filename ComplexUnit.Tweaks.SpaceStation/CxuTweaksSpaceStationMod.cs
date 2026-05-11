using Mafi;
using Mafi.Collections;
using Mafi.Core;
using Mafi.Core.Game;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Core.SpaceProgram;
using Mafi.Serialization;


namespace ComplexUnit.Tweaks.SpaceStation
{
    public sealed class CxuTweaksSpaceStationMod : IMod
    {
        private string Name => nameof(CxuTweaksSpaceStationMod);
        public int Version => 1;

        public ModManifest Manifest { get; }
        public bool IsUiOnly => false;
        public Option<IConfig> ModConfig => Option<IConfig>.None;
        public ModJsonConfig JsonConfig { get; }



        public int ConstructionCostFirstTier => this.JsonConfig.GetInt("construction_cost_first_tier");
        public int ConstructionCostPerTier => this.JsonConfig.GetInt("construction_cost_per_tier");
        public double MaintenancePerMonthPerTier => this.JsonConfig.GetDouble("maintenance_per_month_per_tier");
        public double CrewSuppliesPerMemberPerMonth => this.JsonConfig.GetDouble("crew_supplies_per_member_per_month");
        public int ResearchPointsProvidedPerMonthPerTier => this.JsonConfig.GetInt("research_points_provided_per_month_per_tier");
        public int ResearchSuppliesConsumedPerMonthPerTier => this.JsonConfig.GetInt("research_supplies_consumed_per_month_per_tier");
        public double UnityBonusFirstTier => this.JsonConfig.GetDouble("unity_bonus_first_tier");
        public double UnityBonusPerTier => this.JsonConfig.GetDouble("unity_bonus_per_tier");
        public double ResearchEfficiencyBonusFirstTier => this.JsonConfig.GetDouble("research_efficiency_bonus_first_tier");
        public double ResearchEfficiencyBonusPerTier => this.JsonConfig.GetDouble("research_efficiency_bonus_per_tier");
        public int CrewRotationDuration => this.JsonConfig.GetInt("crew_rotation_duration");



        public CxuTweaksSpaceStationMod(ModManifest manifest)
        {
            this.Manifest = manifest;
            this.JsonConfig = new ModJsonConfig(this);

            Log.Info($"{this.Name}: Mod loaded!");
        }



        public void EarlyInit(DependencyResolver resolver)
        {
            return;
        }

        public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
        {
            return;
        }

        public void MigrateJsonConfig(VersionSlim savedVersion, Dict<string, object> savedValues)
        {
            return;
        }

        public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool gameWasLoaded)
        {
            return;
        }

        public void RegisterPrototypes(ProtoRegistrator registrator)
        {
            Log.Info($"{this.Name}: Registering new maintenance values for SpaceStation...");

            SpaceStationProto spacestation = registrator.PrototypesDb.Get<SpaceStationProto>(IdsCore.SpaceProgram.SpaceStation).Value;
            
            ReflectionUtils.SetField<SpaceStationProto>(spacestation, "m_constructionCostFirstTier", this.ConstructionCostFirstTier.Quantity());
            ReflectionUtils.SetField<SpaceStationProto>(spacestation, "m_constructionCostPerTier", this.ConstructionCostPerTier.Quantity());
            ReflectionUtils.SetField<SpaceStationProto>(spacestation, "m_maintenancePerMonthPerTier", this.MaintenancePerMonthPerTier.Quantity());
            ReflectionUtils.SetField<SpaceStationProto>(spacestation, "m_crewSuppliesPerMemberPerMonth", this.CrewSuppliesPerMemberPerMonth.Quantity());
            ReflectionUtils.SetField<SpaceStationProto>(spacestation, "m_researchPointsProvidedPerMonthPerTier", this.ResearchPointsProvidedPerMonthPerTier.ToFix32());
            ReflectionUtils.SetField<SpaceStationProto>(spacestation, "m_researchSuppliesConsumedPerMonthPerTier", this.ResearchSuppliesConsumedPerMonthPerTier.ToFix32());
            ReflectionUtils.SetField<SpaceStationProto>(spacestation, "m_unityBonusFirstTier", this.UnityBonusFirstTier.Upoints());
            ReflectionUtils.SetField<SpaceStationProto>(spacestation, "m_unityBonusPerTier", this.UnityBonusPerTier.Upoints());
            ReflectionUtils.SetField<SpaceStationProto>(spacestation, "m_researchEfficiencyBonusFirstTier", this.ResearchEfficiencyBonusFirstTier.Percent());
            ReflectionUtils.SetField<SpaceStationProto>(spacestation, "m_researchEfficiencyBonusPerTier", this.ResearchEfficiencyBonusPerTier.Percent());
            
            Utility.SetStaticField(typeof(SpaceStationProto), "CREW_ROTATION_DURATION", this.CrewRotationDuration.Years());

            Log.Info($"{this.Name}: Finished registering new maintenance values.");
        }

        public void Dispose()
        {
            return;
        }
    }
}
