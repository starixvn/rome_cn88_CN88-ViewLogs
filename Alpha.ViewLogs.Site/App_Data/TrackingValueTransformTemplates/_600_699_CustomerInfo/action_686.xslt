<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:output method="html" indent="no" />
  
  <xsl:template match="r">
    <table class="tblDesc">
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('username')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@subaccname" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('Name')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@fname" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('language')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@language" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('Permissions')" />
        </td>
        <td class="colW2" colspan="5">
          <xsl:value-of select="@pageaccess" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('IP')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="xslLogFormat:FormatIP(@loginip)" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('errCode')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@errcode" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('errMsg')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@errmsg" />
        </td>
      </tr>
      <xsl:choose>
        <xsl:when test="$IsInternal = 0">
        </xsl:when>
        <xsl:otherwise>
          <tr>
            <td class="colW1">
              <xsl:value-of select="xslLogFormat:GetLogResource('subAccID')" />
            </td>
            <td class="colW2">
              <xsl:value-of select="@subaccid" />
            </td>
            <xsl:if test="$is-show-empty-cell">
              <td class="colW1"></td>
              <td class="colW2"></td>
              <td class="colW1"></td>
              <td class="colW2"></td>
            </xsl:if>
          </tr>
        </xsl:otherwise>
      </xsl:choose>
    </table>
  </xsl:template>
</xsl:stylesheet>
