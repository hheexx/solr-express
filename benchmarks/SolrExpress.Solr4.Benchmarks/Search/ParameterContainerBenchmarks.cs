﻿using BenchmarkDotNet.Attributes;
using Moq;
using SolrExpress.Benchmarks.Helper;
using SolrExpress.Core.Search;
using SolrExpress.Solr4.Search;
using System.Collections.Generic;

namespace SolrExpress.Benchmarks.Solr4.Search
{
    public class ParameterContainerBenchmarks
    {
        private ISearchParameterCollection<TestDocument> _searchParameterCollection;

        [Params(10, 100, 500, 1000)]
        public int ElementsCount { get; set; }

        [Setup]
        public void Setup()
        {
            var parameters = new List<ISearchParameter>();

            for (int i = 0; i < this.ElementsCount; i++)
            {
                var parameter = new Mock<ISearchParameter>();
                parameter.Setup(q => q.AllowMultipleInstances).Returns(true);
                var parameterExecute = parameter.As<ISearchParameterExecute<List<string>>>();
                parameterExecute.Setup(q => q.Execute(It.IsAny<List<string>>())).Callback((List<string> list) => list.Add("X"));

                parameters.Add((ISearchParameter)parameterExecute.Object);
            }
            this._searchParameterCollection = new SearchParameterCollection<TestDocument>();
            this._searchParameterCollection.Add(parameters);
        }

        [Benchmark]
        public void Execute()
        {
            this._searchParameterCollection.Execute();
        }
    }
}
