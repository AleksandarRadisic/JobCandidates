using Microsoft.EntityFrameworkCore.Migrations;

namespace JobCandidates.Persistence.Migrations
{
    public partial class SkillSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO public." + "\"Skills\"" +
                                 "(\"Id\", \"Name\") VALUES " +
                                 "('178a2c93-d574-42ed-9dba-343a8b3f1d3a', 'Java programming')," +
                                 "('2075dcae-47f5-464c-aae1-bcf5be339f7e', 'C# programming')," +
                                 "('64943aad-9282-49a2-825b-f9a76b83c450', 'German')," +
                                 "('9cdd10a3-9812-454c-8c35-94c2db1aa409', 'English')," +
                                 "('71e740de-08f3-4c74-b417-f171d49a55b1', 'Russian')," +
                                 "('5e21d464-6c74-492b-aadb-ff777e1aa2bd', 'Database design')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM public.\"Skills\"");
        }
    }
}
