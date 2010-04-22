using HtmlTags;

namespace Test
{
    public class TestThisPluginAction
    {
        public HtmlTag  Query(TestThisPluginModel model)
        {
            return new HtmlTag("div").Text("Hello world!");
        }
    }

    public class TestThisPluginModel
    {
    }
}