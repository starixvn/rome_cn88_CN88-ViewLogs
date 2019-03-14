<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:param name="IsInternal" select="0"></xsl:param>
  <xsl:output method="html" indent="no" />
  <xsl:key name="product-by-sport" match="c" use="@sport" />
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
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('product')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="xslLogFormat:GetProductName(@productid)" />
        </td>
      </tr>
      <xsl:for-each select="c[generate-id() = generate-id(key('product-by-sport', @sport)[1])]">
        <tr class="action-name" onclick="slideToggle(this);">
          <td colspan="6">
            <xsl:value-of select="xslLogFormat:GetCN88SportName(@sport)" />
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
                  <xsl:choose>
                    <xsl:when test="@comm">
                      <tr>
                        <td class="colW1">
                          <xsl:value-of select="xslLogFormat:GetLogResource('v204discountvalue_commission')" />
                        </td>
                        <td class="colW2">
                          <xsl:value-of select="@comm" />
                        </td>
                        <td class="colW1">
                        </td>
                        <td  class="colW2">
                        </td>
                        <td class="colW1">
                        </td>
                        <td class="colW2">
                        </td>
                      </tr>
                    </xsl:when>
                    <xsl:otherwise>
                      <tr>
                        <td class="colW1">
                          <xsl:value-of select="xslLogFormat:GetLogResource('groupa')" />
                        </td>
                        <td class="colW2">
                          <xsl:value-of select="@gra" />
                        </td>
                        <td class="colW1">
                          <xsl:value-of select="xslLogFormat:GetLogResource('groupb')" />
                        </td>
                        <td  class="colW2">
                          <xsl:value-of select="@grb" />
                        </td>
                        <td class="colW1">
                          <xsl:value-of select="xslLogFormat:GetLogResource('groupc')" />
                        </td>
                        <td class="colW2">
                          <xsl:value-of select="@grc" />
                        </td>
                      </tr>
                      <tr>
                        <td class="colW1">
                          <xsl:value-of select="xslLogFormat:GetLogResource('groupd')" />
                        </td>
                        <td class="colW2">
                          <xsl:value-of select="@grd" />
                        </td>
                        <td class="colW1">
                        </td>
                        <td  class="colW2">
                        </td>
                        <td class="colW1">
                        </td>
                        <td class="colW2">
                        </td>
                      </tr>
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
