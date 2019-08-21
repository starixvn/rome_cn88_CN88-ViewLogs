<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:param name="IsInternal" select="0"></xsl:param>
  <xsl:output method="html" indent="no" />
  <xsl:variable name="is-mobile" select="xslLogFormat:IsMobile()" />

  <xsl:template match="acc_customerupdmoveagent">
    <table class="tblDesc">
      <tr>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('agentName')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@agentname" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('newMasterName')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="@newmastername" />
        </td>
        <td class="colW1">
          <xsl:value-of select="xslLogFormat:GetLogResource('ip')" />
        </td>
        <td class="colW2">
          <xsl:value-of select="xslLogFormat:FormatIP(@ip)" />
        </td>
      </tr>
      <tr>
        <xsl:choose>
          <xsl:when test="$IsInternal = 0">
            <td class="colW1"></td>
            <td class="colW2"></td>
          </xsl:when>
          <xsl:otherwise>
            <td class="colW1">
              <xsl:value-of select="xslLogFormat:GetLogResource('oldMasterId')" />
            </td>
            <td class="colW2">
              <xsl:value-of select="@oldmasterid" />
            </td>
          </xsl:otherwise>
        </xsl:choose>
        <xsl:choose>
          <xsl:when test="$IsInternal = 0">
            <td class="colW1"></td>
            <td class="colW2"></td>
          </xsl:when>
          <xsl:otherwise>
            <td class="colW1">
              <xsl:value-of select="xslLogFormat:GetLogResource('newMasterId')" />
            </td>
            <td class="colW2">
              <xsl:value-of select="@newmasterid" />
            </td>
          </xsl:otherwise>
        </xsl:choose>
        <xsl:if test="is-mobile ='true'">
          <td class="colW1"></td>
          <td class="colW2"></td>
        </xsl:if>
      </tr>
    </table>
  </xsl:template>
</xsl:stylesheet>
