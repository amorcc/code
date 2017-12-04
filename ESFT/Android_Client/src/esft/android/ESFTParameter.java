package esft.android;

public class ESFTParameter {
	// 参数名称
	public String m_ParameterName;

	// 参数内容
	public String m_ParaContent;

	public ESFTParameter() {

	}

	public ESFTParameter(String iParaName, String iParaContent) {
		this.m_ParameterName = iParaName;
		this.m_ParaContent = iParaContent;
	}
}
