<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:key name="product-by-sport" match="c" use="@sport" />

  <xsl:template name="addOrUpdatePossess">
    <xsl:param name="isAgent" select="true()"/>
    <xsl:param name="actionId"/>
    <table class="tblDesc">
      <xsl:call-template name="renderGeneralInformationRows"/>
      <xsl:for-each select="c[generate-id() = generate-id(key('product-by-sport', @sport)[1])]">
        <tr class="action-name" onclick="slideToggle(this);">
          <td colspan="6">
            <xsl:value-of select="xslLogFormat:GetCN88SportName(@sport, $actionId)" />
          </td>
        </tr>
        <tr class="product-by-sport">
          <td colspan="6">
            <div style="display:block;">
              <table  class="tblDesc" >
                <xsl:for-each select="key('product-by-sport', @sport)">
                  <xsl:choose>
                    <xsl:when test="$isAgent = 'true'">
                      <xsl:call-template name="renderRow">
                        <xsl:with-param name="firstKey" select="'MasterPTCN88'" />
                        <xsl:with-param name="firstValue" select="@masterpossess" />

                        <xsl:with-param name="secondKey" select="'AgentPTCN88'" />
                        <xsl:with-param name="secondValue" select="@agentpossess" />
                      </xsl:call-template>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:call-template name="renderRow">
                        <xsl:with-param name="firstKey" select="'SuperPTCN88'" />
                        <xsl:with-param name="firstValue" select="@superpossess" />
                      </xsl:call-template>
                    </xsl:otherwise>
                  </xsl:choose>
                </xsl:for-each>
              </table>
            </div>
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>
