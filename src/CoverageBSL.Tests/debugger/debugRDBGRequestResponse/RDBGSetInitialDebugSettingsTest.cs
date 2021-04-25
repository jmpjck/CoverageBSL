﻿using com.github.yukon39.CoverageBSL.httpDebug;
using com.github.yukon39.DebugBSL.debugger.debugRDBGRequestResponse;
using com.github.yukon39.DebugBSL.debugger.debugRTEFilter;
using NUnit.Framework;
using System;

namespace CoverageBSL.Tests.debugger.debugRDBGRequestResponse
{
    class RDBGSetInitialDebugSettingsTest
    {

        [Test]
        public void TestRequestSerialization()
        {
            // Given
            var request = new RDBGSetInitialDebugSettingsRequest
            {
                InfoBaseAlias = "DefAlias",
                IdOfDebuggerUI = Guid.Parse("dbe7b1e9-9786-4a25-8da8-304684fa2ce3"),
                Data = new HTTPServerInitialDebugSettingsData()
                {
                    RTEProcessing = new RteFilterStorage()
                    {
                        StopOnErrors = true,
                        AnalyzeErrorStr = true
                    }
                }
            };

            // When
            var xmlString = HTTPDebugSerializer.Serialize(request);
            Console.Write(xmlString);

            // Then
            var o = HTTPDebugSerializer.Deserialize<RDBGSetInitialDebugSettingsRequest>(xmlString);
            Assert.AreEqual(request.InfoBaseAlias, o.InfoBaseAlias);
            Assert.AreEqual(request.IdOfDebuggerUI, o.IdOfDebuggerUI);
            Assert.AreEqual(request.Data.RTEProcessing.StopOnErrors, o.Data.RTEProcessing.StopOnErrors);
            Assert.AreEqual(request.Data.RTEProcessing.AnalyzeErrorStr, o.Data.RTEProcessing.AnalyzeErrorStr);
        }

        [Test]
        public void TestRequestDeserialization()
        {
            // Given
            var xmlString = UtilsTest.XmlString("debugger", "debugRDBGRequestResponse", "RDBGAttachDetachDebugTargetsRequest.xml");

            // When
            var request = HTTPDebugSerializer.Deserialize<RDBGSetInitialDebugSettingsRequest>(xmlString);

            // Then
            Assert.AreEqual(request.InfoBaseAlias, "DefAlias");
            Assert.AreEqual(request.IdOfDebuggerUI, Guid.Parse("dbe7b1e9-9786-4a25-8da8-304684fa2ce3"));
        }

    }
}
