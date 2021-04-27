﻿using com.github.yukon39.DebugBSL.debugger.debugBaseData;
using com.github.yukon39.DebugBSL.debugger.debugRDBGRequestResponse;
using NUnit.Framework;
using System;

namespace com.github.yukon39.DebugBSL.Tests.debugger.debugRDBGRequestResponse
{
    class RDBGAttachDetachDebugTargetsTest
    {
        [Test]
        public void TestRequestSerialization()
        {
            // Given
            var request = new RDBGAttachDetachDebugTargetsRequest
            {
                InfoBaseAlias = "DefAlias",
                IdOfDebuggerUI = Guid.Parse("dbe7b1e9-9786-4a25-8da8-304684fa2ce3"),
                Attach = true
            };
            request.ID.Add(new DebugTargetIdLight() { ID = Guid.Parse("f8849103-dbcd-4984-905d-28059c33a720") });

            // When
            var xmlString = DebuggerXmlSerializer.Serialize(request);
            Console.Write(xmlString);

            // Then
            var xmlRequest = DebuggerXmlSerializer.Deserialize<RDBGAttachDetachDebugTargetsRequest>(xmlString);
            Assert.AreEqual(request.InfoBaseAlias, xmlRequest.InfoBaseAlias);
            Assert.AreEqual(request.IdOfDebuggerUI, xmlRequest.IdOfDebuggerUI);
            Assert.AreEqual(request.Attach, xmlRequest.Attach);
            Assert.AreEqual(request.ID[0].ID, xmlRequest.ID[0].ID);
        }

        [Test]
        public void TestRequestDeserialization()
        {
            // Given
            var xmlString = UtilsTest.XmlString("debugger", "debugRDBGRequestResponse",
                "RDBGAttachDetachDebugTargetsRequest.xml");

            // When
            var request = DebuggerXmlSerializer.Deserialize<RDBGAttachDetachDebugTargetsRequest>(xmlString);

            // Then
            Assert.AreEqual(request.InfoBaseAlias, "DefAlias");
            Assert.AreEqual(request.IdOfDebuggerUI, Guid.Parse("dbe7b1e9-9786-4a25-8da8-304684fa2ce3"));
            Assert.True(request.Attach);
            Assert.AreEqual(request.ID[0].ID, Guid.Parse("f8849103-dbcd-4984-905d-28059c33a720"));
        }
    }
}
