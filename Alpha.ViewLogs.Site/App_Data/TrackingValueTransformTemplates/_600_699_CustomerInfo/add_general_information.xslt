<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />

  <xsl:template name="addGeneralInformation">
    <xsl:param name="isMember" select="true()"/>
    <table class="tblDesc">
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'Name'" />
        <xsl:with-param name="firstValue" select="@name" />
        
        <xsl:with-param name="secondKey" select="'username'" />
        <xsl:with-param name="secondValue" select="@username" />
        
        <xsl:with-param name="thirdKey" select="'credit'" />
        <xsl:with-param name="thirdValue" select="xslLogFormat:FormatDecimal(@credit, 'N2')" />
      </xsl:call-template>

      <xsl:choose>
        <xsl:when test="$isMember = 'true'">
          <xsl:call-template name="renderRow">
            <xsl:with-param name="firstKey" select="'group'" />
            <xsl:with-param name="firstValue" select="@ugroup" />

            <xsl:with-param name="secondKey" select="'subaccname'" />
            <xsl:with-param name="secondValue" select="@subacc" />

            <xsl:with-param name="thirdKey" select="'IP'" />
            <xsl:with-param name="thirdValue" select="@ip" />
          </xsl:call-template>
        </xsl:when>
        <xsl:otherwise>
          <xsl:call-template name="renderRow">
            <xsl:with-param name="firstKey" select="'subaccname'" />
            <xsl:with-param name="firstValue" select="@subacc" />

            <xsl:with-param name="secondKey" select="'IP'" />
            <xsl:with-param name="secondValue" select="xslLogFormat:FormatIP(@ip)" />
          </xsl:call-template>
        </xsl:otherwise>
      </xsl:choose>
    </table>
  </xsl:template>
</xsl:stylesheet>