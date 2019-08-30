<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:key name="product-by-sport" match="c" use="@sport" />

  <xsl:template name="addOrUpdateBetLimit">
    <xsl:param name="actionId"/>
    <table class="tblDesc">
      <tr class="action-name">
        <td colspan="6">
          <xsl:value-of select="xslLogFormat:GetLogResource('GeneralInformation')" />
        </td>
      </tr>
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'subaccname'" />
        <xsl:with-param name="firstValue" select="@subacc" />

        <xsl:with-param name="secondKey" select="'IP'" />
        <xsl:with-param name="secondValue" select="@ip" />
      </xsl:call-template>
      
      <xsl:for-each select="c[generate-id() = generate-id(key('product-by-sport', @sport)[1])]">
        <tr class="action-name" onclick="slideToggle(this);">
          <td colspan="6">
            <xsl:value-of select="xslLogFormat:GetCN88SportName(@sport, $actionId)" />
          </td>
        </tr>
        <tr>
          <td colspan="6">
            <div style="display:block;">
              <table  class="tblDesc" >
                <xsl:for-each select="key('product-by-sport', @sport)">
                  <tr class="headerSportTypeDetail">
                    <td colspan="6">
                      <xsl:value-of select="xslLogFormat:GetCN88BettypeName(@sport, @bt, @inplay)" />
                    </td>
                  </tr>
                  <xsl:call-template name="renderRow">
                    <xsl:with-param name="firstKey" select="'MaxBet'" />
                    <xsl:with-param name="firstValue" select="xslLogFormat:FormatDecimal(@max, 'N0')" />

                    <xsl:with-param name="secondKey" select="'MaxPerMatch'" />
                    <xsl:with-param name="secondValue" select="xslLogFormat:FormatDecimal(@mpe, 'N0')" />
                  </xsl:call-template>
                </xsl:for-each>
              </table>
            </div>
          </td>
        </tr>
      </xsl:for-each>
    </table>
  </xsl:template>
</xsl:stylesheet>
