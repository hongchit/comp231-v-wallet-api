USE [v-wallet-db]
GO
DELETE FROM [tbl_FinancialTransaction]
GO
DELETE FROM [tbl_Category]
GO
DELETE FROM [tbl_FinancialAccount]
GO
DELETE FROM [tbl_FinancialAccountType]
GO
DELETE FROM [tbl_Currency]
GO
DELETE FROM [tbl_UserProfile]
GO
DELETE FROM [tbl_UserAccount]
GO
INSERT [tbl_UserAccount] ([Id], [Email], [Password], [AccountType]) VALUES (N'11324cfd-16b3-476d-9df9-37665b2d5bc8', N'jcborlagdan@outlook.com', N'Password1', N'User')
GO
INSERT [tbl_UserAccount] ([Id], [Email], [Password], [AccountType]) VALUES (N'4ecf8d30-bc7b-4477-85c3-4e492d88740c', N'v-wallet@admin.com', N'5x1&3MRi\t?&', N'Admin')
GO
INSERT [tbl_UserProfile] ([Id], [Firstname], [Lastname], [Birthdate], [UserAccountId]) VALUES (N'833673d4-9c7f-4dfb-b062-87f4532b925c', N'System', N'Admin', NULL, N'4ecf8d30-bc7b-4477-85c3-4e492d88740c')
GO
INSERT [tbl_UserProfile] ([Id], [Firstname], [Lastname], [Birthdate], [UserAccountId]) VALUES (N'150cb5dd-7f3a-4bbb-838d-fb6940298448', N'Jc', N'Borlagdan', NULL, N'11324cfd-16b3-476d-9df9-37665b2d5bc8')
GO
INSERT [tbl_Currency] ([Id], [Symbol], [Country], [IsActive]) VALUES (N'09cba794-33bd-4c78-a167-93bc4b593783', N'CAD', N'Canada', 1)
GO
INSERT [tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'62115192-d968-49cb-ba3c-78ce4d2b142d', N'Loan', NULL)
GO
INSERT [tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'3ea0097b-5c67-4322-b6ae-7d4dd9e436bd', N'Savings', NULL)
GO
INSERT [tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'03edd126-968f-4fb0-bb05-abbfa566e76d', N'General', NULL)
GO
INSERT [tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'ea3b2b04-54b4-4ddd-9ab7-e2d0d6f346ca', N'Chequing', N'Chequing')
GO
INSERT [tbl_FinancialAccountType] ([Id], [Name], [Description]) VALUES (N'4fb16ee4-a08b-4d4d-b113-f14c03711cae', N'Cash', NULL)
GO
INSERT [tbl_FinancialAccount] ([Id], [AccountName], [AccountNumber], [InitialValue], [CurrentValue], [AccountTypeId], [CurrencyId], [UserProfileId]) VALUES (N'71b59d4e-2cd0-4e04-9ba8-589ff00d5692', N'Jesus Carlo', N'12345689', CAST(100 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), N'ea3b2b04-54b4-4ddd-9ab7-e2d0d6f346ca', N'09cba794-33bd-4c78-a167-93bc4b593783', N'150cb5dd-7f3a-4bbb-838d-fb6940298448')
GO
INSERT [tbl_Category] ([Id], [Name], [Description]) VALUES (N'6513dcff-b3d5-453e-9427-4cf2cf57cbd9', N'Shopping', NULL)
GO
INSERT [tbl_Category] ([Id], [Name], [Description]) VALUES (N'02fdf1d2-6878-4199-ae4c-77288ffddb6d', N'Food', NULL)
GO
INSERT [tbl_Category] ([Id], [Name], [Description]) VALUES (N'b32d115b-23a4-4276-bc3d-7c105bd05f97', N'Housing', NULL)
GO
INSERT [tbl_Category] ([Id], [Name], [Description]) VALUES (N'70916aa6-5f82-4b15-9dbd-99c56e55d342', N'Income', NULL)
GO
INSERT [tbl_Category] ([Id], [Name], [Description]) VALUES (N'ef0bf1e3-01cc-457a-9bcc-ba8c1f6df307', N'Transportation', NULL)
GO
INSERT [tbl_FinancialTransaction] ([Id], [Amount], [Description], [TransactionType], [AccountId], [CategoryId], [TransactionDate]) VALUES (N'7e46b34b-9ff4-4949-a357-08dcb560674c', CAST(13 AS Decimal(18, 0)), N'', N'Expense', N'71b59d4e-2cd0-4e04-9ba8-589ff00d5692', N'02fdf1d2-6878-4199-ae4c-77288ffddb6d', CAST(N'2024-08-06T19:07:00.0000000' AS DateTime2))
GO
INSERT [tbl_FinancialTransaction] ([Id], [Amount], [Description], [TransactionType], [AccountId], [CategoryId], [TransactionDate]) VALUES (N'4171700a-7b11-48f1-3b6a-08dcb57426a1', CAST(77 AS Decimal(18, 0)), N'Bus', N'Expense', N'71b59d4e-2cd0-4e04-9ba8-589ff00d5692', N'ef0bf1e3-01cc-457a-9bcc-ba8c1f6df307', CAST(N'2024-08-06T21:28:00.0000000' AS DateTime2))
GO
INSERT [tbl_FinancialTransaction] ([Id], [Amount], [Description], [TransactionType], [AccountId], [CategoryId], [TransactionDate]) VALUES (N'329b66ec-e662-4e45-3b6c-08dcb57426a1', CAST(123 AS Decimal(18, 0)), N'', N'Expense', N'71b59d4e-2cd0-4e04-9ba8-589ff00d5692', N'b32d115b-23a4-4276-bc3d-7c105bd05f97', CAST(N'2024-08-01T21:30:00.0000000' AS DateTime2))
GO
