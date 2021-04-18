﻿using com.github.yukon39.CoverageBSL.debugger.debugBaseData;
using com.github.yukon39.CoverageBSL.debugger.debugDBGUICommands;
using com.github.yukon39.CoverageBSL.debugger.debugRDBGRequestResponse;
using com.github.yukon39.CoverageBSL.httpDebug;
using NUnit.Framework;

namespace CoverageBSL.Tests.debugger.debugDBGUICommands
{
    class DBGUIExtCmdInfoMeasureTest
    {
        [Test]
        public void TestDeserialization()
        {
            // Given
            var xmlString = UtilsTest.XmlString("debugger", "debugDBGUICommands", "DBGUIExtCmdInfoMeasureTest.xml");

            // When
            var response = HTTPDebugSerializer.Deserialize<RDBGPingDebugUIResponse>(xmlString);

            // Then
            Assert.AreEqual(response.Result.Count, 1);
            Assert.IsInstanceOf<DBGUIExtCmdInfoMeasure>(response.Result[0]);
            var infoMeasure = (DBGUIExtCmdInfoMeasure)response.Result[0];

            Assert.IsInstanceOf<DebugTargetId>(infoMeasure.TargetID);
            Assert.AreEqual("DefAlias", infoMeasure.TargetID.InfoBaseAlias);
            Assert.AreEqual("DefAlias", infoMeasure.Measure.TargetID.InfoBaseAlias);
            Assert.Greater(infoMeasure.Measure.TotalDurability, 0);
        }
    }
}