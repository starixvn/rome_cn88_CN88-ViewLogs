<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:param name="IsInternal" select="0"></xsl:param>
  <xsl:output method="html" indent="no" />
  <xsl:variable name="is-show-empty-cell" select="xslLogFormat:ShowEmptyCell()" />

  <xsl:template match="age_customerupduserinfo">
    <table class="tblDesc">
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('firstName')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@firstname" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('lastName')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@lastname" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('subAccName')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@subaccname" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('email')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@email" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('phone')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@phone" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('mobilePhone')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@mobilephone" />
        </td>
      </tr>
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('fax')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@fax" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('ip')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="xslLogFormat:FormatIP(@ip)" />
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
          <xsl:value-of select="xslLogFormat:GetLogResource('errCode')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@errcode" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('errMsg')" />
        </td>
        <td class="colW2" >
          <xsl:value-of select="@errmsg" />
        </td>
        <xsl:if test="$is-show-empty-cell">
          <td class="colW1"></td>
          <td class="colW2"></td>
        </xsl:if>
      </tr>
    </table>
  </xsl:template>
</xsl:stylesheet>
