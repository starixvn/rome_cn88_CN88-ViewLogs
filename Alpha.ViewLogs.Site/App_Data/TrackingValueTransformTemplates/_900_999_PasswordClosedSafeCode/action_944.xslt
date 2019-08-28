<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:param name="IsInternal" select="0"></xsl:param>
  <xsl:output method="html" indent="no" />
  <xsl:variable name="is-show-empty-cell" select="xslLogFormat:ShowEmptyCell()" />

  <xsl:template match="r">
    <table class="tblDesc">
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('language')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@language" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('ip')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="xslLogFormat:FormatIP(@ip)" />
        </td>
        <xsl:if test="$is-show-empty-cell">
          <td class="colW1"></td>
          <td class="colW2"></td>
        </xsl:if>
      </tr>
    </table>
  </xsl:template>
</xsl:stylesheet>
