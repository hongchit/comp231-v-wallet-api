USE [v-wallet-db]
GO
DELETE FROM [dbo].[tbl_FinancialTransaction]
GO
DELETE FROM [dbo].[tbl_Category]
GO
DELETE FROM [dbo].[tbl_FinancialAccount]
GO
DELETE FROM [dbo].[tbl_FinancialAccountType]
GO
DELETE FROM [dbo].[tbl_Currency]
GO
DELETE FROM [dbo].[tbl_UserProfile]
GO
DELETE FROM [dbo].[tbl_UserAccount]
GO
INSERT [dbo].[tbl_UserAccount] ([Id], [Email], [Password], [AccountType]) VALUES (N'11324cfd-16b3-476d-9df9-37665b2d5bc8', N'jcborlagdan@outlook.com', N'Password1', N'User')
GO
INSERT [dbo].[tbl_UserAccount] ([Id], [Email], [Password], [AccountType]) VALUES (N'4ecf8d30-bc7b-4477-85c3-4e492d88740c', N'v-wallet@admin.com', N'5x1&3MRi\t?&', N'Admin')
GO
INSERT [dbo].[tbl_UserProfile] ([Id], [Firstname], [Lastname], [Birthdate], [UserAccountId]) VALUES (N'833673d4-9c7f-4dfb-b062-87f4532b925c', N'System', N'Admin', NULL, N'4ecf8d30-bc7b-4477-85c3-4e492d88740c')
GO
INSERT [dbo].[tbl_UserProfile] ([Id], [Firstname], [Lastname], [Birthdate], [UserAccountId]) VALUES (N'150cb5dd-7f3a-4bbb-838d-fb6940298448', N'Jc', N'Borlagdan', NULL, N'11324cfd-16b3-476d-9df9-37665b2d5bc8')
GO
INSERT [dbo].[tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'62115192-d968-49cb-ba3c-78ce4d2b142d', N'Loan', NULL)
GO
INSERT [dbo].[tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'3ea0097b-5c67-4322-b6ae-7d4dd9e436bd', N'Savings', NULL)
GO
INSERT [dbo].[tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'03edd126-968f-4fb0-bb05-abbfa566e76d', N'General', NULL)
GO
INSERT [dbo].[tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'ea3b2b04-54b4-4ddd-9ab7-e2d0d6f346ca', N'Chequing', NULL)
GO
INSERT [dbo].[tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'4fb16ee4-a08b-4d4d-b113-f14c03711cae', N'Cash', NULL)
GO
INSERT [dbo].[tbl_Category] ([Id], [Name], [Description]) VALUES (N'6513dcff-b3d5-453e-9427-4cf2cf57cbd9', N'Shopping', NULL)
GO
INSERT [dbo].[tbl_Category] ([Id], [Name], [Description]) VALUES (N'02fdf1d2-6878-4199-ae4c-77288ffddb6d', N'Food', NULL)
GO
INSERT [dbo].[tbl_Category] ([Id], [Name], [Description]) VALUES (N'b32d115b-23a4-4276-bc3d-7c105bd05f97', N'Housing', NULL)
GO
INSERT [dbo].[tbl_Category] ([Id], [Name], [Description]) VALUES (N'70916aa6-5f82-4b15-9dbd-99c56e55d342', N'Income', NULL)
GO
INSERT [dbo].[tbl_Category] ([Id], [Name], [Description]) VALUES (N'ef0bf1e3-01cc-457a-9bcc-ba8c1f6df307', N'Transportation', NULL)
GO
