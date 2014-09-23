using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KudevolveWeb.ServiceOperations
{
    public class CountyItems
    {
        public CountyItem[] countydata { get; set; }
    }

    public class CountyItem
    {
        public string county_name { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public General_Information General_Information { get; set; }
        public Health_And_Education_Outcomes Health_and_Education_Outcomes { get; set; }
        public Funding_Per_Capital_In_Kshs_20102011 Funding_Per_Capital_in_Kshs_20102011 { get; set; }
        public Access_To_Infrastructure Access_to_infrastructure { get; set; }
        public Service_Coverage Service_Coverage { get; set; }
        public CDF_Allocations_Kshs_Millions CDF_allocations_Kshs_Millions { get; set; }
        public Rural_Electrification_Allocations_Kshs_Millions Rural_Electrification_Allocations_Kshs_Millions { get; set; }
        public Rural_Electrification_Allocations_KshsMillions Rural_Electrification_Allocations_KshsMillions { get; set; }
    }

    public class General_Information
    {
        public string Population2009 { get; set; }
        public string Population1999 { get; set; }
        public string Annual_population_growth_rate19992009 { get; set; }
        public string Surface_Areakm2 { get; set; }
        public string Population_Density_2009people_per_km2 { get; set; }
        public string Poverty_gap_based_on_KIHBS200506 { get; set; }
        public string Share_of_urban_population_2009 { get; set; }
        public string Annual_population_growth_rate_19992009 { get; set; }
    }

    public class Health_And_Education_Outcomes
    {
        public string Fullyimmunized_pop1yr_1213 { get; set; }
        public string Malaria_burden_2012 { get; set; }
        public string TB_cases_in_every_100000_people2012 { get; set; }
        public string HIV_prevalence_in_2011 { get; set; }
        public string People_Living_With_HIV2011 { get; set; }
        public string New_HIV_infections2011 { get; set; }
        public string Population_With_Primary_Education { get; set; }
        public string Population_with_secondary_education { get; set; }
        public string People_Living_With_2011 { get; set; }
    }

    public class Funding_Per_Capital_In_Kshs_20102011
    {
        public string Constituency_Development_FundCDF { get; set; }
        public string Local_Authority_Transfer_FundLATF { get; set; }
        public string Single_Business_Permit_revenues_by_LAs { get; set; }
        public string property_tax_revenues_by_LAs1999 { get; set; }
        public string Rural_Electrification_Programme_Fund2009 { get; set; }
        public string Total { get; set; }
    }

    public class Access_To_Infrastructure
    {
        public string Improved_water_households_2009 { get; set; }
        public string Improved_sanitation_households_2009 { get; set; }
        public string Electricity_households_2009 { get; set; }
        public string Paved_roads_total_roads_2012 { get; set; }
    }

    public class Service_Coverage
    {
        public string Delivered_in_a_health_centre { get; set; }
        public string Qualified_edical_assistant_during_birth { get; set; }
        public string Had_all_vaccinations { get; set; }
        public string Adequate_height_for_age { get; set; }
        public string Can_read_and_write { get; set; }
        public string Attending_school_1518years_old { get; set; }
        public string Qualified_medical_assistant_duringbirth { get; set; }
        public string Attending_school_1518yearsold { get; set; }
    }

    public class CDF_Allocations_Kshs_Millions
    {
        public string _0506 { get; set; }
        public string _0607 { get; set; }
        public string _0708 { get; set; }
        public string _0809 { get; set; }
        public string _0910 { get; set; }
        public string _1011 { get; set; }
        public string _1112 { get; set; }
    }

    public class Rural_Electrification_Allocations_Kshs_Millions
    {
        public string _0607 { get; set; }
        public string _0708 { get; set; }
        public string _0809 { get; set; }
        public string _0910 { get; set; }
        public string _1011 { get; set; }
        public string _1112 { get; set; }
    }

    public class Rural_Electrification_Allocations_KshsMillions
    {
        public string _0607 { get; set; }
        public string _0708 { get; set; }
        public string _0809 { get; set; }
        public string _0910 { get; set; }
        public string _1011 { get; set; }
        public string _1112 { get; set; }
    }

}