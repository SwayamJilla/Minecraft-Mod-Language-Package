﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Serilog;
using Serilog.Core;

namespace Processer
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            var folder = ReaderFolder();
            var langFiles = new List<LangFile>();
            var config = ReaderConfig(folder.Config);
            if (config.RunDelFiles)
            {
            }

            if (config.RunSortFiles)
            {
                //Utils.DelDeduplicationFiles(path, config.TargetVersion);
                var files = Utils.SearchAllFiles("./", config.TargetVersion);
                files.ForEach(_ => langFiles.Add(new LangFile(_, config.ModBlackList, config.PathBlackList)));
                //langFiles.ForEach(_ => Log.Information("路径:{0}语言:{1}是否需要处理：{2}",_.LangPath,_.Language,_.IsNeeded));
                langFiles.ForEach(_ => _.ProcessLangFile(_));
            }
        }

        static Config ReaderConfig(string path)
        {
            var reader = File.ReadAllBytes(path + "/processer.json");
            return JsonSerializer.Deserialize<Config>(reader);
        }

        static Folder ReaderFolder()
        {
            var reader = File.ReadAllBytes("./config/folder.json");
            return JsonSerializer.Deserialize<Folder>(reader);
        }
    }
}