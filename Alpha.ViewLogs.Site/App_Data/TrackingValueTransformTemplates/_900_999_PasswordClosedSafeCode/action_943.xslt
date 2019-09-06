<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:import href="../helper.xslt" />
  <xsl:output method="html" indent="no" />

  <xsl:template match="r">
    <table class="tblDesc">
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'CheckedSecurityCode'" />
        <xsl:with-param name="firstValue" select="@checkedsecuritycode" />

        <xsl:with-param name="secondKey" select="'FunctionName'" />
        <xsl:with-param name="secondValue" select="@functionname" />

        <xsl:with-param name="thirdKey" select="'language'" />
        <xsl:with-param name="thirdValue" select="@language" />
      </xsl:call-template>

      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'ip'" />
        <xsl:with-param name="firstValue" select="@ip" />
      </xsl:call-template>
    </table>
  </xsl:template>
</xsl:stylesheet>
