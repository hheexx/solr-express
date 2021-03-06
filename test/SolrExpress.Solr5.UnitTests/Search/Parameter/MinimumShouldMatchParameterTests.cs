﻿using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Search.Parameter;
using System;
using Xunit;

namespace SolrExpress.Solr5.UnitTests.Search.Parameter
{
    public class MinimumShouldMatchParameterTests
    {
        /// <summary>
        /// Where   Using a MinimumShouldMatchParameter instance
        /// When    Invoking the method "Execute"
        /// What    Create a valid JSON
        /// </summary>
        [Fact]
        public void MinimumShouldMatchParameter001()
        {
            // Arrange
            var expected = JObject.Parse(@"
            {
              params:{
                mm:""75%""
              }
            }");
            string actual;
            var jObject = new JObject();
            var parameter = new MinimumShouldMatchParameter<TestDocument>();
            parameter.Configure("75%");

            // Act
            parameter.Execute(jObject);
            actual = jObject.ToString();

            // Assert
            Assert.Equal(expected.ToString(), actual);
        }

        /// <summary>
        /// Where   Using a MinimumShouldMatchParameter instance
        /// When    Create the instance with null
        /// What    Throws ArgumentNullException
        /// </summary>
        [Fact]
        public void MinimumShouldMatchParameter002()
        {
            // Arrange
            var parameter = new MinimumShouldMatchParameter<TestDocument>();

            // Act / Assert
            Assert.Throws<ArgumentNullException>(() => parameter.Configure(null));
        }
    }
}
