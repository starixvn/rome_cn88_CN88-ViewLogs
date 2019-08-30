<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:key name="product-by-sport" match="c" use="@sport" />

  <xsl:template name="addOrUpdateCommission">
    <table class="tblDesc">
      <xsl:call-template name="renderGeneralInformationRows"/>
      <xsl:for-each select="c[generate-id() = generate-id(key('product-by-sport', @sport)[1])]">
        <xsl:call-template name="renderSportNameRow"/>
        <tr class="product-by-sport">
          <td colspan="6">
            <div style="display:block;">
              <table  class="tblDesc" >
                <xsl:for-each select="key('product-by-sport', @sport)">
                  <xsl:call-template name="renderBettypeRow"/>
                  <xsl:choose>
                    <xsl:when test="@comm">
                      <xsl:call-template name="renderRow">
                        <xsl:with-param name="firstKey" select="'v204discountvalue_commission'" />
                        <xsl:with-param name="firstValue" select="@comm" />
                      </xsl:call-template>
                    </xsl:when>
                    <xsl:otherwise>
                      <xsl:call-template name="renderRow">
                        <xsl:with-param name="firstKey" select="'groupa'" />
                        <xsl:with-param name="firstValue" select="@gra" />

                        <xsl:with-param name="secondKey" select="'groupb'" />
                        <xsl:with-param name="secondValue" select="@grb" />

                        <xsl:with-param name="thirdKey" select="'groupc'" />
                        <xsl:with-param name="thirdValue" select="@grc" />
                      </xsl:call-template>
                      <xsl:call-template name="renderRow">
                        <xsl:with-param name="firstKey" select="'groupd'" />
                        <xsl:with-param name="firstValue" select="@grd" />
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
