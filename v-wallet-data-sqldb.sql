USE [v-wallet-db]
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
