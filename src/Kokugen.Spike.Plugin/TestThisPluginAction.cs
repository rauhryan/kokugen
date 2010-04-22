using FubuMVC.Core.View;
using HtmlTags;

namespace Test
{
    public class TestThisPluginAction
    {
        public TestThisPluginModel Query(TestThisPluginModel model)
        {
            return model;
        }
    }

    public class TestThisPluginModel
    {
    }

    public class Test : FubuPage<TestThisPluginModel>{}
}