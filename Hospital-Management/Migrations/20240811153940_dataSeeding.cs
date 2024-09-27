using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hospital_Management.Migrations
{
    /// <inheritdoc />
    public partial class dataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "762d0b1c-a588-4689-bc38-9358793a065c", null, "Assistant", "ASSISTANT" },
                    { "af794439-1eed-439a-9cfb-c34299c93c6a", null, "Patient", "PATIENT" },
                    { "c063f579-bcdc-43a9-a614-38af82aa5a78", null, "Admin", "ADMIN" },
                    { "dd371c53-9e6e-438f-8ac7-a8a80518451e", null, "Doctor", "DOCTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AbscentTimes", "AccessFailedCount", "Address", "BirthDate", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "Img", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "45913f68-a3ed-4b02-b211-a7b997196477", 0, 0, null, new DateTime(2006, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "6539c8ca-e674-45d6-a4d2-add1dbcb3e48", "Patient", "patient4@example.com", true, "Noah", null, "Taylor", false, null, "PATIENT4@EXAMPLE.COM", "PATIENT4@EXAMPLE.COM", "AQAAAAIAAYagAAAAEP2ZNGk1RQTHloetHDeh2rOQ3HWh/JIL/IBlwqZEvI3rNrlvJLx+fE+6zVvQ2zE8LQ==", null, false, "", false, "patient4@example.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BirthDate", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "Img", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "45d139e1-cf2f-46d8-b1ca-133ca9d5a893", 0, "456 Admin Blvd", new DateTime(1975, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "c29ad074-0a57-437a-8324-4de447676e24", "ApplicationUser", "admin2@example.com", true, "Master", null, "Admin", false, null, "ADMIN2@EXAMPLE.COM", "ADMIN2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEC1LFtbS+WxEvByDCHy+CLoVLRYYe5GqhN9TNBWKAmlIPqNjuU/+oBaW/pPx0qAz7Q==", null, false, "", false, "admin2@example.com" },
                    { "a7a963d9-de3b-43f9-9ee0-ef69d145ad46", 0, "123 Admin St", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "03df19a5-3e07-41f2-8c56-89645a734823", "ApplicationUser", "admin1@example.com", true, "Super", null, "Admin", false, null, "ADMIN1@EXAMPLE.COM", "ADMIN1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKPjztjgrSz4ICm6ljzZUnY147KO2KqH/v8HFLHTzfQI/qcvZMnIdrMeIePM+6JSDQ==", null, false, "", false, "admin1@example.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AbscentTimes", "AccessFailedCount", "Address", "BirthDate", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "Img", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "c03b3de7-6399-4fe6-ba3c-59eba5c058bb", 0, 0, null, new DateTime(2002, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "42ba9f22-3db4-4ac6-9c3e-114adcb73f81", "Patient", "patient2@example.com", true, "Sophia", null, "Miller", false, null, "PATIENT2@EXAMPLE.COM", "PATIENT2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEA8zcJBbn9DqEecbSWAlR4i/SliCWpOr2iV1epcDE2ox5JSH0VcpA0asJy0evQ+Udw==", null, false, "", false, "patient2@example.com" },
                    { "db671643-7876-42fb-add6-088cc35a7565", 0, 0, null, new DateTime(2004, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "4cbebca8-379b-497b-88df-0bba46889cbb", "Patient", "patient3@example.com", true, "Liam", null, "Wilson", false, null, "PATIENT3@EXAMPLE.COM", "PATIENT3@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMOjYct5DxGgg5aNQF80kQzBBqHruwHDAXJhhFI0URN8iq9UoGmEaIbaLqN/F88gnw==", null, false, "", false, "patient3@example.com" },
                    { "ef824d9b-3dad-482a-9228-11ea067e78d0", 0, 0, null, new DateTime(2008, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "5cd2e7bd-25b1-41cc-bd98-dcd8cf14c98a", "Patient", "patient5@example.com", true, "Emma", null, "Anderson", false, null, "PATIENT5@EXAMPLE.COM", "PATIENT5@EXAMPLE.COM", "AQAAAAIAAYagAAAAEDiCdZlKJEMwSguAoamMUPRIUyqStpY9hqhh3qXXQt9IRHqBNCuPZp270FVJpzu6Mw==", null, false, "", false, "patient5@example.com" },
                    { "f43865ca-95c5-4a30-86fa-ae4d3d4c5377", 0, 0, null, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "55193e7f-f6bc-4042-9f75-72d185a4e955", "Patient", "patient1@example.com", true, "Olivia", null, "Brown", false, null, "PATIENT1@EXAMPLE.COM", "PATIENT1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEHfOSFbUspVdqgT1HO6026sH/mTk3VGQAo5cbA7PGExGexhQv5YknlrHLB2gtrxRDQ==", null, false, "", false, "patient1@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Specialities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cardiology" },
                    { 2, "Diagnosis" },
                    { 3, "Surgery" },
                    { 4, "First Aid" },
                    { 5, "Orthopedics" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "af794439-1eed-439a-9cfb-c34299c93c6a", "45913f68-a3ed-4b02-b211-a7b997196477" },
                    { "c063f579-bcdc-43a9-a614-38af82aa5a78", "45d139e1-cf2f-46d8-b1ca-133ca9d5a893" },
                    { "c063f579-bcdc-43a9-a614-38af82aa5a78", "a7a963d9-de3b-43f9-9ee0-ef69d145ad46" },
                    { "af794439-1eed-439a-9cfb-c34299c93c6a", "c03b3de7-6399-4fe6-ba3c-59eba5c058bb" },
                    { "af794439-1eed-439a-9cfb-c34299c93c6a", "db671643-7876-42fb-add6-088cc35a7565" },
                    { "af794439-1eed-439a-9cfb-c34299c93c6a", "ef824d9b-3dad-482a-9228-11ea067e78d0" },
                    { "af794439-1eed-439a-9cfb-c34299c93c6a", "f43865ca-95c5-4a30-86fa-ae4d3d4c5377" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BirthDate", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "ExaminationFees", "ExaminationsMinutes", "FirstName", "Img", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SpecialityId", "StartHour", "TwoFactorEnabled", "UserName", "WorkingDays", "WorkingHours" },
                values: new object[,]
                {
                    { "3a3ab798-68f3-4673-ac60-836e431ea667", 0, null, new DateTime(1975, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "b37cfd47-0239-45ee-be99-8dce7e34deaf", "Doctor", "doctor3@example.com", true, 300m, 25, "Alice", null, "Johnson", false, null, "DOCTOR3@EXAMPLE.COM", "DOCTOR3@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMRvwPa6wBbQnaRZdR9rztoF8sR5cbPkl0nXK/gbd87uf/vv4U0sMJJLnDuS9e7Hcg==", null, false, "", 3, 8, false, "doctor3@example.com", 18, 7 },
                    { "7003e456-9405-4c8f-b118-105339cfffbb", 0, null, new DateTime(1970, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "703403f3-8e77-4c38-8bc5-eb037bc06133", "Doctor", "doctor4@example.com", true, 180m, 15, "Bob", null, "Williams", false, null, "DOCTOR4@EXAMPLE.COM", "DOCTOR4@EXAMPLE.COM", "AQAAAAIAAYagAAAAEE+6dypJ8EwuP6ju24zT5dgkZ9jrpSWsZZ2OfnHzdJasNLAuCDDW6bwMZ8CMj3QCRQ==", null, false, "", 4, 11, false, "doctor4@example.com", 40, 5 },
                    { "741330ca-7210-4d8b-8526-6ad69ce88275", 0, null, new DateTime(1965, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "1ffcd161-174c-4e98-89eb-086bdb54354b", "Doctor", "doctor5@example.com", true, 220m, 20, "Charlie", null, "Brown", false, null, "DOCTOR5@EXAMPLE.COM", "DOCTOR5@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKdXHLFO0mXc5KRPp8f3k8Dlu12Qpbjr985xLZE3z/N13G1DJ9iVrzhRg9gYMjvLzg==", null, false, "", 5, 9, false, "doctor5@example.com", 20, 9 },
                    { "809a8e0b-a777-41a5-9803-d63d64ffe8bd", 0, null, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e76ffbd3-4e63-46b8-8b92-05ba84789096", "Doctor", "doctor1@example.com", true, 200m, 20, "John", null, "Doe", false, null, "DOCTOR1@EXAMPLE.COM", "DOCTOR1@EXAMPLE.COM", "AQAAAAIAAYagAAAAEKR3QTegVLj/GH9GUmdNjoGlnyFbAtHHvSujdIuf8AzCs+R5rpQc3MCWa+h31fXfvg==", null, false, "", 1, 9, false, "doctor1@example.com", 42, 8 },
                    { "9a277e69-13db-4ff8-8041-93955f153db2", 0, null, new DateTime(1985, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "67fdb479-cc0e-436b-9ef7-2f9c247e5d16", "Doctor", "doctor2@example.com", true, 250m, 30, "Jane", null, "Smith", false, null, "DOCTOR2@EXAMPLE.COM", "DOCTOR2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEF9Z187EKVdim5irW2wgFymHzzY0qZLuBm7KzKsmDUE9jAikq9rH8WCL83RvtYgB8w==", null, false, "", 2, 10, false, "doctor2@example.com", 20, 6 }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Content", "DateTime", "DoctorId", "Title" },
                values: new object[,]
                {
                    { 1, "Regular checkups can help identify potential health issues...", new DateTime(2024, 8, 11, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(664), "809a8e0b-a777-41a5-9803-d63d64ffe8bd", "The Importance of Regular Checkups" },
                    { 2, "A balanced diet is essential for maintaining good health...", new DateTime(2024, 8, 9, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(674), "9a277e69-13db-4ff8-8041-93955f153db2", "Healthy Eating Habits" },
                    { 3, "Stress management techniques are important for overall well-being...", new DateTime(2024, 8, 7, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(689), "3a3ab798-68f3-4673-ac60-836e431ea667", "Managing Stress" },
                    { 4, "Diabetes management is crucial for preventing complications...", new DateTime(2024, 8, 5, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(712), "7003e456-9405-4c8f-b118-105339cfffbb", "Understanding Diabetes" },
                    { 5, "Regular exercise can improve your physical and mental health...", new DateTime(2024, 8, 3, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(719), "741330ca-7210-4d8b-8526-6ad69ce88275", "The Benefits of Exercise" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "dd371c53-9e6e-438f-8ac7-a8a80518451e", "3a3ab798-68f3-4673-ac60-836e431ea667" },
                    { "dd371c53-9e6e-438f-8ac7-a8a80518451e", "7003e456-9405-4c8f-b118-105339cfffbb" },
                    { "dd371c53-9e6e-438f-8ac7-a8a80518451e", "741330ca-7210-4d8b-8526-6ad69ce88275" },
                    { "dd371c53-9e6e-438f-8ac7-a8a80518451e", "809a8e0b-a777-41a5-9803-d63d64ffe8bd" },
                    { "dd371c53-9e6e-438f-8ac7-a8a80518451e", "9a277e69-13db-4ff8-8041-93955f153db2" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "BirthDate", "ConcurrencyStamp", "Discriminator", "DoctorId", "Email", "EmailConfirmed", "FirstName", "Img", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00f20433-9169-4491-920a-8365a01597f7", 0, null, new DateTime(1996, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "99177479-8771-43ea-8151-8a0c6ce3fe37", "Assistant", "7003e456-9405-4c8f-b118-105339cfffbb", "assistant4@example.com", true, "Henry", null, "Baker", false, null, "ASSISTANT4@EXAMPLE.COM", "ASSISTANT4@EXAMPLE.COM", "AQAAAAIAAYagAAAAEALbvEEbJ/SfpOtSoBSB/KgDSXwN6jjctcnzFEFz86xmE1OAhGVccyBcQkURaJXyUQ==", null, false, "", false, "assistant4@example.com" },
                    { "01c0e819-bc9f-4509-92ee-e243ff2002a7", 0, null, new DateTime(1994, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "b55ddddf-0b45-49c4-ba6e-3d6a971525d1", "Assistant", "3a3ab798-68f3-4673-ac60-836e431ea667", "assistant3@example.com", true, "Grace", null, "Harris", false, null, "ASSISTANT3@EXAMPLE.COM", "ASSISTANT3@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAO57hrHPqEh79mr8+r/Zl/5CnYm24X0YOhmu4OzVOao7FXDcjGGB/RVdDBsXhghag==", null, false, "", false, "assistant3@example.com" },
                    { "1f1e3bbc-8e57-4778-b550-66a86eb9d64b", 0, null, new DateTime(1992, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "dd299178-945e-4b82-9fe5-adc2895792ea", "Assistant", "9a277e69-13db-4ff8-8041-93955f153db2", "assistant2@example.com", true, "Frank", null, "Clark", false, null, "ASSISTANT2@EXAMPLE.COM", "ASSISTANT2@EXAMPLE.COM", "AQAAAAIAAYagAAAAEENY+9UBhZkDD5bCqWjBeze3cNIOOE4YySSPJ24qQuheWytCrwdS2bPI8/87iMsJeA==", null, false, "", false, "assistant2@example.com" },
                    { "6fc2b5a6-c8de-44a7-9f13-f8bd5194be42", 0, null, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2f782ad7-df01-4f15-978f-751ab47620a2", "Assistant", "809a8e0b-a777-41a5-9803-d63d64ffe8bd", "assistant1@example.com", true, "Eve", null, "Adams", false, null, "ASSISTANT1@EXAMPLE.COM", "ASSISTANT1@EXAMPLE.COM", "AQAAAAIAAYagAAAAELrQbnKRiSmSiwiXMCLfgT1XZJB5CcOkyuUN91nofwVjsCUt+J84PbsLauJKMnTlpg==", null, false, "", false, "assistant1@example.com" },
                    { "e5cbc6af-0097-4b7e-9265-a72275568e4f", 0, null, new DateTime(1998, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "dba0f791-8c9e-41e6-845f-855286dc24ff", "Assistant", "741330ca-7210-4d8b-8526-6ad69ce88275", "assistant5@example.com", true, "Ivy", null, "Davis", false, null, "ASSISTANT5@EXAMPLE.COM", "ASSISTANT5@EXAMPLE.COM", "AQAAAAIAAYagAAAAEJJpDKhiaqLWLVD8IM1PJ5GyyCcvSRUGF8w+gdHObhapmU6sA3qIMf1LsnInurFTIA==", null, false, "", false, "assistant5@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "Comment", "DoctorId", "PatientId", "Value" },
                values: new object[,]
                {
                    { 1, "Excellent service!", "809a8e0b-a777-41a5-9803-d63d64ffe8bd", "f43865ca-95c5-4a30-86fa-ae4d3d4c5377", 5 },
                    { 2, "Very good, but could be quicker.", "9a277e69-13db-4ff8-8041-93955f153db2", "c03b3de7-6399-4fe6-ba3c-59eba5c058bb", 4 },
                    { 3, "Average experience.", "3a3ab798-68f3-4673-ac60-836e431ea667", "db671643-7876-42fb-add6-088cc35a7565", 3 },
                    { 4, "Not satisfied with the consultation.", "7003e456-9405-4c8f-b118-105339cfffbb", "45913f68-a3ed-4b02-b211-a7b997196477", 2 },
                    { 5, "Poor service.", "741330ca-7210-4d8b-8526-6ad69ce88275", "ef824d9b-3dad-482a-9228-11ea067e78d0", 1 }
                });

            migrationBuilder.InsertData(
                table: "Records",
                columns: new[] { "Id", "Date", "Description", "Diagnosis", "DoctorId", "Notes", "PatientId", "Prescription" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 11, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(938), "Annual physical exam", "Healthy", "809a8e0b-a777-41a5-9803-d63d64ffe8bd", "Patient is in good health.", "f43865ca-95c5-4a30-86fa-ae4d3d4c5377", "Continue regular exercise" },
                    { 2, new DateTime(2024, 6, 11, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(954), "Follow-up for hypertension", "Hypertension under control", "9a277e69-13db-4ff8-8041-93955f153db2", "Blood pressure is stable.", "c03b3de7-6399-4fe6-ba3c-59eba5c058bb", "Continue medication" },
                    { 3, new DateTime(2024, 5, 11, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(959), "Consultation for diabetes management", "Type 2 diabetes", "3a3ab798-68f3-4673-ac60-836e431ea667", "Patient is advised to monitor blood sugar levels.", "db671643-7876-42fb-add6-088cc35a7565", "Insulin therapy" },
                    { 4, new DateTime(2024, 4, 11, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(964), "Initial consultation for weight management", "Obesity", "7003e456-9405-4c8f-b118-105339cfffbb", "Patient is advised to follow a structured diet and exercise regimen.", "45913f68-a3ed-4b02-b211-a7b997196477", "Diet and exercise plan" },
                    { 5, new DateTime(2024, 3, 11, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(972), "Consultation for chronic back pain", "Chronic lower back pain", "741330ca-7210-4d8b-8526-6ad69ce88275", "Patient is referred to physical therapy.", "ef824d9b-3dad-482a-9228-11ea067e78d0", "Physical therapy and pain management" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "762d0b1c-a588-4689-bc38-9358793a065c", "00f20433-9169-4491-920a-8365a01597f7" },
                    { "762d0b1c-a588-4689-bc38-9358793a065c", "01c0e819-bc9f-4509-92ee-e243ff2002a7" },
                    { "762d0b1c-a588-4689-bc38-9358793a065c", "1f1e3bbc-8e57-4778-b550-66a86eb9d64b" },
                    { "762d0b1c-a588-4689-bc38-9358793a065c", "6fc2b5a6-c8de-44a7-9f13-f8bd5194be42" },
                    { "762d0b1c-a588-4689-bc38-9358793a065c", "e5cbc6af-0097-4b7e-9265-a72275568e4f" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "AssistantId", "Date", "DoctorId", "PatientId", "RateId", "ReservationStatus" },
                values: new object[,]
                {
                    { 1, "6fc2b5a6-c8de-44a7-9f13-f8bd5194be42", new DateTime(2024, 8, 18, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(1117), "809a8e0b-a777-41a5-9803-d63d64ffe8bd", "f43865ca-95c5-4a30-86fa-ae4d3d4c5377", null, 0 },
                    { 2, "1f1e3bbc-8e57-4778-b550-66a86eb9d64b", new DateTime(2024, 8, 25, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(1129), "9a277e69-13db-4ff8-8041-93955f153db2", "c03b3de7-6399-4fe6-ba3c-59eba5c058bb", null, 0 },
                    { 3, "01c0e819-bc9f-4509-92ee-e243ff2002a7", new DateTime(2024, 9, 1, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(1133), "3a3ab798-68f3-4673-ac60-836e431ea667", "db671643-7876-42fb-add6-088cc35a7565", null, 0 },
                    { 4, "00f20433-9169-4491-920a-8365a01597f7", new DateTime(2024, 9, 8, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(1137), "7003e456-9405-4c8f-b118-105339cfffbb", "45913f68-a3ed-4b02-b211-a7b997196477", null, 0 },
                    { 5, "e5cbc6af-0097-4b7e-9265-a72275568e4f", new DateTime(2024, 9, 15, 18, 39, 39, 839, DateTimeKind.Local).AddTicks(1141), "741330ca-7210-4d8b-8526-6ad69ce88275", "ef824d9b-3dad-482a-9228-11ea067e78d0", null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "762d0b1c-a588-4689-bc38-9358793a065c", "00f20433-9169-4491-920a-8365a01597f7" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "762d0b1c-a588-4689-bc38-9358793a065c", "01c0e819-bc9f-4509-92ee-e243ff2002a7" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "762d0b1c-a588-4689-bc38-9358793a065c", "1f1e3bbc-8e57-4778-b550-66a86eb9d64b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dd371c53-9e6e-438f-8ac7-a8a80518451e", "3a3ab798-68f3-4673-ac60-836e431ea667" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "af794439-1eed-439a-9cfb-c34299c93c6a", "45913f68-a3ed-4b02-b211-a7b997196477" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c063f579-bcdc-43a9-a614-38af82aa5a78", "45d139e1-cf2f-46d8-b1ca-133ca9d5a893" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "762d0b1c-a588-4689-bc38-9358793a065c", "6fc2b5a6-c8de-44a7-9f13-f8bd5194be42" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dd371c53-9e6e-438f-8ac7-a8a80518451e", "7003e456-9405-4c8f-b118-105339cfffbb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dd371c53-9e6e-438f-8ac7-a8a80518451e", "741330ca-7210-4d8b-8526-6ad69ce88275" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dd371c53-9e6e-438f-8ac7-a8a80518451e", "809a8e0b-a777-41a5-9803-d63d64ffe8bd" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dd371c53-9e6e-438f-8ac7-a8a80518451e", "9a277e69-13db-4ff8-8041-93955f153db2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c063f579-bcdc-43a9-a614-38af82aa5a78", "a7a963d9-de3b-43f9-9ee0-ef69d145ad46" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "af794439-1eed-439a-9cfb-c34299c93c6a", "c03b3de7-6399-4fe6-ba3c-59eba5c058bb" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "af794439-1eed-439a-9cfb-c34299c93c6a", "db671643-7876-42fb-add6-088cc35a7565" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "762d0b1c-a588-4689-bc38-9358793a065c", "e5cbc6af-0097-4b7e-9265-a72275568e4f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "af794439-1eed-439a-9cfb-c34299c93c6a", "ef824d9b-3dad-482a-9228-11ea067e78d0" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "af794439-1eed-439a-9cfb-c34299c93c6a", "f43865ca-95c5-4a30-86fa-ae4d3d4c5377" });

            migrationBuilder.DeleteData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Records",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "762d0b1c-a588-4689-bc38-9358793a065c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af794439-1eed-439a-9cfb-c34299c93c6a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c063f579-bcdc-43a9-a614-38af82aa5a78");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd371c53-9e6e-438f-8ac7-a8a80518451e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00f20433-9169-4491-920a-8365a01597f7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "01c0e819-bc9f-4509-92ee-e243ff2002a7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1f1e3bbc-8e57-4778-b550-66a86eb9d64b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45913f68-a3ed-4b02-b211-a7b997196477");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "45d139e1-cf2f-46d8-b1ca-133ca9d5a893");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fc2b5a6-c8de-44a7-9f13-f8bd5194be42");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a7a963d9-de3b-43f9-9ee0-ef69d145ad46");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c03b3de7-6399-4fe6-ba3c-59eba5c058bb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "db671643-7876-42fb-add6-088cc35a7565");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e5cbc6af-0097-4b7e-9265-a72275568e4f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ef824d9b-3dad-482a-9228-11ea067e78d0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f43865ca-95c5-4a30-86fa-ae4d3d4c5377");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3a3ab798-68f3-4673-ac60-836e431ea667");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7003e456-9405-4c8f-b118-105339cfffbb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "741330ca-7210-4d8b-8526-6ad69ce88275");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "809a8e0b-a777-41a5-9803-d63d64ffe8bd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9a277e69-13db-4ff8-8041-93955f153db2");

            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specialities",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
