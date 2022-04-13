CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "ActivityTypes" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "UserUniqId" uuid NOT NULL,
    CONSTRAINT "PK_ActivityTypes" PRIMARY KEY ("Id")
);

CREATE TABLE "Projects" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text NOT NULL,
    "UserUniqId" uuid NOT NULL,
    CONSTRAINT "PK_Projects" PRIMARY KEY ("Id")
);

CREATE TABLE "UserActivities" (
    "Id" uuid NOT NULL,
    "StartTime" timestamp without time zone NOT NULL,
    "EndTime" timestamp without time zone NOT NULL,
    "Status" integer NOT NULL,
    "Title" text NOT NULL,
    "Comment" text NOT NULL,
    "ProjectId" uuid NOT NULL,
    "ActivityTypeId" uuid NOT NULL,
    "UserUniqId" uuid NOT NULL,
    CONSTRAINT "PK_UserActivities" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_UserActivities_ActivityTypes_ActivityTypeId" FOREIGN KEY ("ActivityTypeId") REFERENCES "ActivityTypes" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_UserActivities_Projects_ProjectId" FOREIGN KEY ("ProjectId") REFERENCES "Projects" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_UserActivities_ActivityTypeId" ON "UserActivities" ("ActivityTypeId");

CREATE INDEX "IX_UserActivities_ProjectId" ON "UserActivities" ("ProjectId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220412024756_init_timelogDb', '6.0.4');

COMMIT;