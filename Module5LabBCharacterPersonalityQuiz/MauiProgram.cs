﻿using Microsoft.Extensions.Logging;

namespace Module5LabBCharacterPersonalityQuiz;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		string dbPath = FileAccessHelper.GetLocalFilePath("questions.db3");

		// File.Delete(dbPath);

		builder.Services.AddSingleton<QuestionRepository>(s => ActivatorUtilities.CreateInstance<QuestionRepository>(s, dbPath));


#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
