
namespace ESFT.Message
{
    /// <summary>
    /// ESFT参数类型
    /// </summary>
    public class ESFTParameter
    {
        string m_parameterName;
        string m_parameterContent;

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParaName
        {
            get { return this.m_parameterName; }
            set { this.m_parameterName = value; }
        }

        /// <summary>
        /// 参数内容
        /// </summary>
        public string ParaContent
        {
            get { return this.m_parameterContent; }
            set { this.m_parameterContent = value; }
        }

        public ESFTParameter()
        {
        }

        public ESFTParameter(string iParaName, string iParaContent)
        {
            this.m_parameterName = iParaName;
            this.m_parameterContent = iParaContent;
        }
    }
}
