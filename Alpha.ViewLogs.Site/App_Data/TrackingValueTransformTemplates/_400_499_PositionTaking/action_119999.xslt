<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:param name="IsInternal" select="0"></xsl:param>
  <xsl:output method="html" indent="no" />
  <xsl:key name="product-by-sport" match="c" use="@sport" />
  <xsl:variable name="is-mobile" select="xslLogFormat:IsMobile()" />

  <xsl:template match="r">
    <table class="tblDesc">
      <tr class="action-name">
        <td colspan="6">
          <xsl:value-of select="xslLogFormat:GetLogResource('GeneralInformation')" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('subaccname')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@subacc" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('IP')" />
        </td>
        <td  class="colW2">
          <xsl:value-of select="xslLogFormat:FormatIP(@ip)" />
        </td>
        <xsl:if test="is-mobile ='true'">
          <td class="colW1"></td>
          <td class="colW2"></td>
        </xsl:if>
      </tr>
      <xsl:for-each select="c[generate-id() = generate-id(key('product-by-sport', @sport)[1])]">
        <tr class="action-name" onclick="slideToggle(this);">
          <td colspan="6">
            <xsl:value-of select="xslLogFormat:GetCN88SportName(@sport, 119999)" />
          </td>
        </tr>
        <tr class="product-by-sport">
          <td colspan="6">
            <div style="display:block;">
              <table  class="tblDesc" >
                <xsl:for-each select="key('product-by-sport', @sport)">
                  <tr>
                    <td class="colW1">
                      <xsl:value-of select="xslLogFormat:GetLogResource('MasterPTCN88')" />
                    </td>
                    <td class="colW2">
                      <xsl:value-of select="@masterpossess" />
                    </td>
                    <td class="colW1">
                      <xsl:value-of select="xslLogFormat:GetLogResource('AgentPTCN88')" />
                    </td>
                    <td class="colW2">
                      <xsl:value-of select="@agentpossess" />
                    </td>
                    <xsl:if test="is-mobile ='true'">
                      <td class="colW1"></td>
                      <td class="colW2"></td>
                    </xsl:if>
                  </tr>
                </xsl:for-each>
              </table>
            </div>
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>
