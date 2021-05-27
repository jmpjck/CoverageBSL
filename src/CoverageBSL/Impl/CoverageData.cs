﻿using com.github.yukon39.DebugBSL.debugger.debugMeasure;
using ScriptEngine.HostedScript.Library;
using ScriptEngine.HostedScript.Library.Json;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Values;
using System.Collections.Generic;

namespace com.github.yukon39.CoverageBSL.Coverage.Impl
{
    public class CoverageData : ICoverageData
    {
        public long TotalDurability { get; set; }

        public int TotalLines { get; set; }

        public MapImpl Data { get; } = new MapImpl();

        public void AddRange(List<PerformanceInfoMain> performanceInfos)
        {
            performanceInfos.ForEach(x => ProcessPerformanceInfo(x));
        }

        private void ProcessPerformanceInfo(PerformanceInfoMain info)
        {
            TotalDurability += info.TotalDurability;
            info.ModuleData.ForEach(x => ProcessPerformanceInfoModule(x));
        }

        private void ProcessPerformanceInfoModule(PerformanceInfoModule module)
        {
            var moduleBSL = new CoverageModuleId(module.ModuleID);
            var linesCoverage = Data.Retrieve(moduleBSL) as MapImpl;
            if (linesCoverage is null)
            {
                linesCoverage = new MapImpl();
                Data.Insert(moduleBSL, linesCoverage);
            }

            module.LineInfo.ForEach(x => ProcessPerformanceInfoLine(x, linesCoverage));
        }

        private void ProcessPerformanceInfoLine(PerformanceInfoLine line, MapImpl lineslinesCoverage) =>
            lineslinesCoverage.Insert(NumberValue.Create(line.LineNo), BooleanValue.True);
    }
}