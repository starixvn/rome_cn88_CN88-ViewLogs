<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:key name="product-by-sport" match="c" use="@sport" />

  <xsl:template name="addOrUpdateBetLimit">
    <xsl:param name="actionId"/>
    <table class="tblDesc">
      <xsl:call-template name="renderGeneralInformationRows"/>
      <xsl:for-each select="c[generate-id() = generate-id(key('product-by-sport', @sport)[1])]">
        <xsl:call-template name="renderSportNameRow">
          <xsl:with-param name="actionId" select="$actionId" />
        </xsl:call-template>
        <tr>
          <td colspan="6">
            <div style="display:block;">
              <table class="tblDesc" >
                <xsl:for-each select="key('product-by-sport', @sport)">
                  <xsl:call-template name="renderBettypeRow"/>
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
