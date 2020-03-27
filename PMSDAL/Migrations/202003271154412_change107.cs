namespace PMSDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change107 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RecordBondingHistories", "InstructionCode");
            DropColumn("dbo.RecordBondingHistories", "TargetAppearance");
            DropColumn("dbo.RecordBondingHistories", "TargetWarpageCheck");
            DropColumn("dbo.RecordBondingHistories", "TargetThicknessCheck");
            DropColumn("dbo.RecordBondingHistories", "TargetDiameterCheck");
            DropColumn("dbo.RecordBondingHistories", "TargetPerson");
            DropColumn("dbo.RecordBondingHistories", "TargetCheckTime");
            DropColumn("dbo.RecordBondingHistories", "PlateMaterial");
            DropColumn("dbo.RecordBondingHistories", "PlateBelong");
            DropColumn("dbo.RecordBondingHistories", "PlateDimension");
            DropColumn("dbo.RecordBondingHistories", "PlateUseCount");
            DropColumn("dbo.RecordBondingHistories", "PlateHardness");
            DropColumn("dbo.RecordBondingHistories", "PlateSuplier");
            DropColumn("dbo.RecordBondingHistories", "PlateLastWeldMaterial");
            DropColumn("dbo.RecordBondingHistories", "PlateOtherRecord");
            DropColumn("dbo.RecordBondingHistories", "PlateAppearance");
            DropColumn("dbo.RecordBondingHistories", "PlatePerson");
            DropColumn("dbo.RecordBondingHistories", "PlateCheckTime");
            DropColumn("dbo.RecordBondingHistories", "TargetPreProcessRecord");
            DropColumn("dbo.RecordBondingHistories", "TargetPreProcessPerson");
            DropColumn("dbo.RecordBondingHistories", "TargetPreProcessCheckTime");
            DropColumn("dbo.RecordBondingHistories", "PlatePreProcessRecord");
            DropColumn("dbo.RecordBondingHistories", "PlatePreProcessPerson");
            DropColumn("dbo.RecordBondingHistories", "PlatePreProcessCheckTime");
            DropColumn("dbo.RecordBondingHistories", "WeldMaterial");
            DropColumn("dbo.RecordBondingHistories", "WeldCuStringDiameter");
            DropColumn("dbo.RecordBondingHistories", "WeldHold");
            DropColumn("dbo.RecordBondingHistories", "WeldPerson");
            DropColumn("dbo.RecordBondingHistories", "WeldCheckTime");
            DropColumn("dbo.RecordBondingHistories", "WarpageFix");
            DropColumn("dbo.RecordBondingHistories", "WarpagePerson");
            DropColumn("dbo.RecordBondingHistories", "WarpageCheckTime");
            DropColumn("dbo.RecordBondingHistories", "DimensionCheck");
            DropColumn("dbo.RecordBondingHistories", "DimensionWarpageCheck");
            DropColumn("dbo.RecordBondingHistories", "DimensionPerson");
            DropColumn("dbo.RecordBondingHistories", "DimensionCheckTime");
            DropColumn("dbo.RecordBondingHistories", "BindingCheck");
            DropColumn("dbo.RecordBondingHistories", "BindingPerson");
            DropColumn("dbo.RecordBondingHistories", "BindingCheckTime");
            DropColumn("dbo.RecordBondingHistories", "SprayCheck");
            DropColumn("dbo.RecordBondingHistories", "SprayPerson");
            DropColumn("dbo.RecordBondingHistories", "SprayCheckTime");
            DropColumn("dbo.RecordBondingHistories", "CleanCheck");
            DropColumn("dbo.RecordBondingHistories", "CleanPerson");
            DropColumn("dbo.RecordBondingHistories", "CleanCheckTime");
            DropColumn("dbo.RecordBondingHistories", "ApperanceCheck");
            DropColumn("dbo.RecordBondingHistories", "ApperancePerson");
            DropColumn("dbo.RecordBondingHistories", "ApperanceCheckTime");
            DropColumn("dbo.RecordBondingHistories", "PackCheck");
            DropColumn("dbo.RecordBondingHistories", "PackPerson");
            DropColumn("dbo.RecordBondingHistories", "PackCheckTime");
            DropColumn("dbo.RecordBondings", "InstructionCode");
            DropColumn("dbo.RecordBondings", "TargetAppearance");
            DropColumn("dbo.RecordBondings", "TargetWarpageCheck");
            DropColumn("dbo.RecordBondings", "TargetThicknessCheck");
            DropColumn("dbo.RecordBondings", "TargetDiameterCheck");
            DropColumn("dbo.RecordBondings", "TargetPerson");
            DropColumn("dbo.RecordBondings", "TargetCheckTime");
            DropColumn("dbo.RecordBondings", "PlateMaterial");
            DropColumn("dbo.RecordBondings", "PlateBelong");
            DropColumn("dbo.RecordBondings", "PlateDimension");
            DropColumn("dbo.RecordBondings", "PlateUseCount");
            DropColumn("dbo.RecordBondings", "PlateHardness");
            DropColumn("dbo.RecordBondings", "PlateSuplier");
            DropColumn("dbo.RecordBondings", "PlateLastWeldMaterial");
            DropColumn("dbo.RecordBondings", "PlateOtherRecord");
            DropColumn("dbo.RecordBondings", "PlateAppearance");
            DropColumn("dbo.RecordBondings", "PlatePerson");
            DropColumn("dbo.RecordBondings", "PlateCheckTime");
            DropColumn("dbo.RecordBondings", "TargetPreProcessRecord");
            DropColumn("dbo.RecordBondings", "TargetPreProcessPerson");
            DropColumn("dbo.RecordBondings", "TargetPreProcessCheckTime");
            DropColumn("dbo.RecordBondings", "PlatePreProcessRecord");
            DropColumn("dbo.RecordBondings", "PlatePreProcessPerson");
            DropColumn("dbo.RecordBondings", "PlatePreProcessCheckTime");
            DropColumn("dbo.RecordBondings", "WeldMaterial");
            DropColumn("dbo.RecordBondings", "WeldCuStringDiameter");
            DropColumn("dbo.RecordBondings", "WeldHold");
            DropColumn("dbo.RecordBondings", "WeldPerson");
            DropColumn("dbo.RecordBondings", "WeldCheckTime");
            DropColumn("dbo.RecordBondings", "WarpageFix");
            DropColumn("dbo.RecordBondings", "WarpagePerson");
            DropColumn("dbo.RecordBondings", "WarpageCheckTime");
            DropColumn("dbo.RecordBondings", "DimensionCheck");
            DropColumn("dbo.RecordBondings", "DimensionWarpageCheck");
            DropColumn("dbo.RecordBondings", "DimensionPerson");
            DropColumn("dbo.RecordBondings", "DimensionCheckTime");
            DropColumn("dbo.RecordBondings", "BindingCheck");
            DropColumn("dbo.RecordBondings", "BindingPerson");
            DropColumn("dbo.RecordBondings", "BindingCheckTime");
            DropColumn("dbo.RecordBondings", "SprayCheck");
            DropColumn("dbo.RecordBondings", "SprayPerson");
            DropColumn("dbo.RecordBondings", "SprayCheckTime");
            DropColumn("dbo.RecordBondings", "CleanCheck");
            DropColumn("dbo.RecordBondings", "CleanPerson");
            DropColumn("dbo.RecordBondings", "CleanCheckTime");
            DropColumn("dbo.RecordBondings", "ApperanceCheck");
            DropColumn("dbo.RecordBondings", "ApperancePerson");
            DropColumn("dbo.RecordBondings", "ApperanceCheckTime");
            DropColumn("dbo.RecordBondings", "PackCheck");
            DropColumn("dbo.RecordBondings", "PackPerson");
            DropColumn("dbo.RecordBondings", "PackCheckTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecordBondings", "PackCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "PackPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "PackCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "ApperanceCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "ApperancePerson", c => c.String());
            AddColumn("dbo.RecordBondings", "ApperanceCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "CleanCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "CleanPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "CleanCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "SprayCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "SprayPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "SprayCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "BindingCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "BindingPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "BindingCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "DimensionCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "DimensionPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "DimensionWarpageCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "DimensionCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "WarpageCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "WarpagePerson", c => c.String());
            AddColumn("dbo.RecordBondings", "WarpageFix", c => c.String());
            AddColumn("dbo.RecordBondings", "WeldCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "WeldPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "WeldHold", c => c.String());
            AddColumn("dbo.RecordBondings", "WeldCuStringDiameter", c => c.String());
            AddColumn("dbo.RecordBondings", "WeldMaterial", c => c.String());
            AddColumn("dbo.RecordBondings", "PlatePreProcessCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "PlatePreProcessPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "PlatePreProcessRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetPreProcessCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "TargetPreProcessPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetPreProcessRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "PlatePerson", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateAppearance", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateOtherRecord", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateLastWeldMaterial", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateSuplier", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateHardness", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateUseCount", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateDimension", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateBelong", c => c.String());
            AddColumn("dbo.RecordBondings", "PlateMaterial", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondings", "TargetPerson", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetDiameterCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetThicknessCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetWarpageCheck", c => c.String());
            AddColumn("dbo.RecordBondings", "TargetAppearance", c => c.String());
            AddColumn("dbo.RecordBondings", "InstructionCode", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PackCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondingHistories", "PackPerson", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PackCheck", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "ApperanceCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondingHistories", "ApperancePerson", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "ApperanceCheck", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "CleanCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondingHistories", "CleanPerson", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "CleanCheck", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "SprayCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondingHistories", "SprayPerson", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "SprayCheck", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "BindingCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondingHistories", "BindingPerson", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "BindingCheck", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "DimensionCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondingHistories", "DimensionPerson", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "DimensionWarpageCheck", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "DimensionCheck", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "WarpageCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondingHistories", "WarpagePerson", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "WarpageFix", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "WeldCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondingHistories", "WeldPerson", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "WeldHold", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "WeldCuStringDiameter", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "WeldMaterial", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PlatePreProcessCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondingHistories", "PlatePreProcessPerson", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PlatePreProcessRecord", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "TargetPreProcessCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondingHistories", "TargetPreProcessPerson", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "TargetPreProcessRecord", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PlateCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondingHistories", "PlatePerson", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PlateAppearance", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PlateOtherRecord", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PlateLastWeldMaterial", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PlateSuplier", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PlateHardness", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PlateUseCount", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PlateDimension", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PlateBelong", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "PlateMaterial", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "TargetCheckTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.RecordBondingHistories", "TargetPerson", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "TargetDiameterCheck", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "TargetThicknessCheck", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "TargetWarpageCheck", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "TargetAppearance", c => c.String());
            AddColumn("dbo.RecordBondingHistories", "InstructionCode", c => c.String());
        }
    }
}
